using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DarkMidiRemapperCore;
using Microsoft.Win32;
using NAudio.Midi;
using System.Collections.ObjectModel;
using System.IO;

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
        
        MidiFilePath = dlg.FileName;
        _midiFile = new MidiFile(MidiFilePath, false);
        IsMidiFileLoaded = true;

        var mappings = _remapper.GetMappings(_midiFile);

        Mappings.Clear();
        foreach (var mapping in mappings)
        {
            Mappings.Add(mapping);
        }
    }

    [RelayCommand]
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
    }

    [RelayCommand]
    private void Clear()
    {
        IsMidiFileLoaded = false;
        MidiFilePath = null;
        _midiFile = null;
    }
}
