using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DarkMidiRemapperCore;
using Microsoft.Win32;
using NAudio.Midi;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace DarkMidiRemapperDesktop;

public partial class HomeViewModel : ObservableObject
{
    private readonly Remapper _remapper = new();

    [ObservableProperty]
    private bool _isMidiFileLoaded;

    [ObservableProperty]
    private string? _midiFilePath;

    private MidiFile? _midiFile;

    public ObservableCollection<Mapping> Mappings { get; } = new();

    [RelayCommand]
    private void Load()
    {
        var dlg = new OpenFileDialog
        {
            FileName = "MIDI",
            DefaultExt = ".mid",
            Filter = "MIDI files (.mid)|*.mid"
        };

        var result = dlg.ShowDialog();

        if (result is false)
        {
            return;
        }

        try
        {
            _midiFile = new MidiFile(dlg.FileName, false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Failed to load MIDI file");
            return;
        }

        MidiFilePath = dlg.FileName;
        IsMidiFileLoaded = true;
        SaveCommand.NotifyCanExecuteChanged();
        ClearCommand.NotifyCanExecuteChanged();

        var mappings = _remapper.GetMappings(_midiFile);

        Mappings.Clear();
        foreach (var mapping in mappings)
        {
            Mappings.Add(mapping);
        }
    }

    [RelayCommand(CanExecute = nameof(IsMidiFileLoaded))]
    private void Save()
    {
        if(_midiFile is null)
        {
            return;
        }

        var fileName = Path.GetFileNameWithoutExtension(_midiFilePath);

        var dlg = new SaveFileDialog
        {
            FileName = $"{fileName}_remapped.mid",
            DefaultExt = ".mid",
            Filter = "MIDI files (.mid)|*.mid"
        };

        var result = dlg.ShowDialog();

        if (result is false)
        {
            return;
        }

        string targetMidiFilePath = dlg.FileName;

        _remapper.AlterMapping(_midiFile, Mappings);

        MidiFile.Export(targetMidiFilePath, _midiFile.Events);

        MessageBox.Show($"Done! Re-mapped MIDI file was saved to {targetMidiFilePath}", "Success");
    }

    [RelayCommand(CanExecute = nameof(IsMidiFileLoaded))]
    private void Clear()
    {
        IsMidiFileLoaded = false;
        MidiFilePath = null;
        _midiFile = null;
        Mappings.Clear();
        SaveCommand.NotifyCanExecuteChanged();
        ClearCommand.NotifyCanExecuteChanged();
    }
}
