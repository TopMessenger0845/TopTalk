﻿
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace TopTalk.ViewModels
{
    public class CommandPack
    {
        public Dictionary<string, (string Description, Func<string, Task> Handler)> Commands { get; set; } = [];
        public CommandPack AddCommand(string command, string description, Func<string, Task> handler)
        {
            ArgumentNullException.ThrowIfNull(command);
            ArgumentNullException.ThrowIfNull(description);
            ArgumentNullException.ThrowIfNull(handler);

            if (string.IsNullOrWhiteSpace(command) || !command.StartsWith('/'))
                throw new ArgumentException("Команда должна начинаться с '/' и не быть пустой.", nameof(command));

            if (!Commands.TryAdd(command, (description, handler)))
                throw new ArgumentException($"Команда '{command}' уже существует.");

            return this;
        }
    }

    public class ChatCommandProcessor
    {
        private readonly ConcurrentDictionary<string, (string Description, Func<string, Task> Handler)> _commands = [];

        /// <summary>
        /// Добавляет команду с обработчиком в процессор.
        /// </summary>
        /// <param name="command">Команда (например, "/help").</param>
        /// <param name="description">Описание команды.</param>
        /// <param name="handler">Обработчик команды.</param>
        /// <exception cref="ArgumentNullException">Если команда, описание или обработчик пусты.</exception>
        /// <exception cref="ArgumentException">Если команда уже существует.</exception>
        public ChatCommandProcessor AddCommand(string command, string description, Func<string, Task> handler)
        {
            ArgumentNullException.ThrowIfNull(command);
            ArgumentNullException.ThrowIfNull(description);
            ArgumentNullException.ThrowIfNull(handler);

            if (string.IsNullOrWhiteSpace(command) || !command.StartsWith('/'))
                throw new ArgumentException("Команда должна начинаться с '/' и не быть пустой.", nameof(command));

            if (!_commands.TryAdd(command, (description, handler)))
                throw new ArgumentException($"Команда '{command}' уже существует.");

            return this;
        }

        public ChatCommandProcessor AddCommandsFrom(CommandPack pack)
        {
            foreach (var command in pack.Commands)
                AddCommand(command.Key, command.Value.Description, command.Value.Handler);

            return this;
        }
        public bool RemoveCommand(string command)
        {
            ArgumentNullException.ThrowIfNull(command);
            return _commands.TryRemove(command.ToLower(), out _);
        }

        public void Clear() => _commands.Clear();

        public void GetHints(string input, ObservableCollection<string> outContainer)
        {
            ArgumentNullException.ThrowIfNull(outContainer);
            outContainer.Clear();

            if (string.IsNullOrWhiteSpace(input) || !input.StartsWith('/'))
                return;

            input = input.ToLower();

            foreach (var hint in _commands)
            {
                int minLength = Math.Min(input.Length, hint.Key.Length);

                if (input[..minLength].Equals(hint.Key[..minLength], StringComparison.OrdinalIgnoreCase))
                    outContainer.Add(hint.Value.Description);
            }
        }

        public bool TryCompleteCommand(string input, out string completedCommand)
        {
            completedCommand = null!;

            if (string.IsNullOrWhiteSpace(input) || !input.StartsWith('/'))
                return false;

            var matches = GetMatches(input);

            if (matches.Count == 1)
            {
                if (input.StartsWith(matches[0]))
                    return false;

                completedCommand = matches[0];
                return true;
            }

            return false;
        }

        private List<string> GetMatches(string input)
        {
            var matches = new List<string>();
            input = input.ToLower();

            foreach (var hint in _commands)
            {
                int minLength = Math.Min(input.Length, hint.Key.Length);

                if (input[..minLength].Equals(hint.Key[..minLength], StringComparison.OrdinalIgnoreCase))
                    matches.Add(hint.Key);
            }

            return matches;
        }

        public async Task ExecuteCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Команда не должна быть пустой.", nameof(input));

            // Разделение команд по "&&" типо консоль из линукса
            var commands = input.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(c => c.Trim())
                                .ToList();

            foreach (var commandInput in commands)
            {
                if (!commandInput.StartsWith('/'))
                    throw new ArgumentException($"Каждая команда должна начинаться с '/'. Некорректная команда: {commandInput}");

                var executed = false;

                foreach (var command in _commands)
                {
                    if (commandInput.StartsWith(command.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            await command.Value.Handler(commandInput);
                            executed = true;
                        }
                        catch
                        {
                            executed = false;
                        }
                        break;
                    }
                }

                if (!executed)
                    throw new KeyNotFoundException($"Команда '{commandInput}' не найдена.");
            }
        }


        public string? GetCommandDescription(string command)
        {
            if (_commands.TryGetValue(command.ToLower(), out var commandInfo))
                return commandInfo.Description;

            return null;
        }
    }
}
