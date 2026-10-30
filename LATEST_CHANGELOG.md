## v1.9.4 (patch)

Changes since v1.9.3:

- Advance the wrap loop through a next index instead of rewinding the counter (Claude)
- Keep a soft-hyphen break within the wrap width [patch] (Claude)
- Use Assert.AreSequenceEqual in the exact-fill word-wrap test (Claude)
- Stop NominalWordWrap wrapping one character early at exact widths (Claude)
- Construct GetOrCreate's new value only when the key is missing (Claude)
- Snapshot AddFrom's items so a collection can add from itself [patch] (Claude)
- Draw one hyphen where a soft hyphen comes before a visible one [patch] (Claude)
- Keep no-break spaces inside words in NominalWordWrap [patch] (Claude)

