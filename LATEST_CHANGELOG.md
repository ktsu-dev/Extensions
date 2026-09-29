## v1.9.0 (minor)

Changes since v1.8.0:

- Do not render a soft hyphen that directly follows a visible hyphen ([@Claude](https://github.com/Claude))
- test: use Assert.AreSequenceEqual in the surrogate-pair wrap tests (MSTEST0068) ([@Claude](https://github.com/Claude))
- test: use Assert.AreSequenceEqual in the unbounded-ratio wrap test (MSTEST0068) ([@Claude](https://github.com/Claude))
- fix: keep surrogate pairs whole when NominalWordWrap hard-breaks a word [patch] ([@Claude](https://github.com/Claude))
- fix: reject NaN widths in NominalWordWrap and clamp an unbounded width ratio [patch] ([@Claude](https://github.com/Claude))
- fix: make Join(items, separator, nullItemHandling) reject a null separator [patch] ([@Claude](https://github.com/Claude))

