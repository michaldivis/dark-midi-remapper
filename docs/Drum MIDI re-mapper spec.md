# Drum MIDI re-mapper spec

**Steps**
- tell me the current mapping
    - find all distinct notes that need to be mapped
    - user marks notes as different drums
- tell me the desired mapping
    - let the user choose a note for each of the drums names
    - start with the current mapping, users changes what they need
- save resulting MIDI

**In greater detail**
- get a MIDI file as an input
- read MIDI file and identify all distinct notes
- return a list of distinct notes that need to be described to get the source mapping
- let the user assign names to all distinct notes
- let the user change notes to get the target mapping
- export tweaked MIDI file when the source mapping is changed to the target mapping

**MappedNote class**
- int SourceMidiNote (filled from the start)
- string Name (filled by the user)
- int TargetMidiNote (filled by the user)