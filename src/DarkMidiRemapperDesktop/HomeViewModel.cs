using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DarkMidiRemapperCore;
using NAudio.Midi;
using System.Collections.ObjectModel;

namespace DarkMidiRemapperDesktop;

public partial class HomeViewModel : ObservableObject
{
    private readonly Remapper _remapper = new();

    private MidiFile? _midiFile;

    public ObservableCollection<Mapping> Mappings { get; } = new();

    [RelayCommand]
    private void Load()
    {
        //TODO get file from open file dialog
        var midiFilePath = @"E:\Projects\DarkMidiRemapper\assets\test_drums.mid";
        _midiFile = new MidiFile(midiFilePath, false);
        var mappings = _remapper.GetMappings(_midiFile);

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

        _remapper.AlterMapping(_midiFile, Mappings);

        //TODO get file from save file dialog
        var targetMidiFilePath = @"C:\Users\Michal\Downloads\test_drums_remapped.mid";

        MidiFile.Export(targetMidiFilePath, _midiFile.Events);
    }
}
