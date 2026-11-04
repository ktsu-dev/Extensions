## v1.10.0 (minor)

Changes since v1.9.0:

- Break NominalWordWrap at every Unicode mandatory line break [patch] (#185) (matt-edmondson)
- Keep a hard-broken hyphen on the line with the rest of its word [patch] (#183) (matt-edmondson)
- Break NominalWordWrap at zero-width spaces [minor] (#181) (matt-edmondson)
- Search an interface's ancestors breadth-first by inheritance distance [patch] (#180) (matt-edmondson)
- Advance the wrap loop through a next index instead of rewinding the counter (Claude)
- Keep a soft-hyphen break within the wrap width [patch] (Claude)
- Use Assert.AreSequenceEqual in the exact-fill word-wrap test (Claude)
- Stop NominalWordWrap wrapping one character early at exact widths (Claude)
- Construct GetOrCreate's new value only when the key is missing (Claude)
- Snapshot AddFrom's items so a collection can add from itself [patch] (Claude)
- Draw one hyphen where a soft hyphen comes before a visible one [patch] (Claude)
- Keep no-break spaces inside words in NominalWordWrap [patch] (Claude)
- [patch] Keep items whose ToString() is null in ToStringEnumerable and Join (Claude)
- [patch] Keep hyphen runs and word-initial hyphens together in NominalWordWrap (Claude)
- [patch] Resolve overloads in TryFindMethod and search inherited interfaces (Claude)
- [patch] Check for nulls during the one enumeration in Join and ToStringEnumerable (Claude)
- Move CI onto the shared ci-shared.yml pipeline (Claude)

