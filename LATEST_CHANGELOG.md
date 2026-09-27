## v1.7.0 (minor)

Changes since v1.6.0:

- Cover the null checks of the one-argument ConcurrentDictionary GetOrCreate ([@Claude](https://github.com/Claude))
- Make GetOrCreate(key) atomic on ConcurrentDictionary ([@Claude](https://github.com/Claude))
- fix: read ReplaceWith's new items before clearing the collection [patch] ([@Claude](https://github.com/Claude))
- fix: make the ConcurrentDictionary GetOrCreate overload atomic [patch] ([@Claude](https://github.com/Claude))
- Gate Dependabot auto-merge on CI actually being green ([@Claude](https://github.com/Claude))
- ci: adopt the consolidated .NET workflow [patch] ([@Claude](https://github.com/Claude))
- ci: make the SonarQube quality gate opt in [patch] ([@matt-edmondson](https://github.com/matt-edmondson))
- ci: adopt the unified dotnet workflow [patch] ([@matt-edmondson](https://github.com/matt-edmondson))
- Always run CI on pull requests ([@matt-edmondson](https://github.com/matt-edmondson))
- chore: store icon.png in LFS as .gitattributes declares ([@matt-edmondson](https://github.com/matt-edmondson))
- docs: scope build badge to the default branch ([@matt-edmondson](https://github.com/matt-edmondson))
- docs: correct README, DESCRIPTION and TAGS metadata ([@matt-edmondson](https://github.com/matt-edmondson))
- Stop Update SDKs failing when there is nothing to update ([@matt-edmondson](https://github.com/matt-edmondson))
- Fix build errors from ktsu.Sdk analyzer update [patch] ([@matt-edmondson](https://github.com/matt-edmondson))
- Sync .editorconfig ([@KtsuTools](https://github.com/KtsuTools))
- Sync global.json ([@KtsuTools](https://github.com/KtsuTools))

