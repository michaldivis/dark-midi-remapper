using DarkMidiRemapperCore;
using DarkMusicConcepts;
using NAudio.Midi;

//TODO get file path from args
var sourceMidiFilePath = "xxx";
var targetMidiFilePath = "xxx";
var mf = new MidiFile(sourceMidiFilePath, false);

var remapper = new Remapper();
var mappings = remapper.GetMappings(mf);

//TODO assign names
mappings[0].Name = "Kick"; //TODO remove this
mappings[1].Name = "Snare"; //TODO remove this

//TODO assign target notes
mappings[0].TargetNote = Note.C2;
mappings[1].TargetNote = Note.D2;

remapper.AlterMapping(mf, mappings);

MidiFile.Export(targetMidiFilePath, mf.Events);