# README Badges

Reusable badge assets for project README files.

## ShieldCN

The [ShieldCN directory](ShieldCN) contains the current static navigation and profile badges in light and dark variants:

- Getting Started, Installation, and Quick Start
- Add To VPM
- Support
- X profile: @SleightlyDev

Use only the navigation labels needed for a project. Set the link destination in the consuming README: installation or support anchors, https://vpm.sleightly.dev for Add To VPM, and https://x.com/SleightlyDev for the profile badge. A static SVG contains appearance, not a clickable destination.

Use GitHub's `<picture>` element to select the dark variant with `prefers-color-scheme: dark`, with the light SVG as the fallback. Raw asset URLs follow this pattern:

`https://raw.githubusercontent.com/JustSleightly/Resources/main/Badges/ShieldCN/quick-start-light.svg`

Keep the SVG's original aspect ratio. Navigation badges are 32px tall; the X badge is 24px tall.

## Live Metadata

These committed SVGs are static. Release versions, download counts, update dates, and Discord member counts require a live data endpoint or an explicitly maintained generation process. A label-only SVG is not a dynamic badge, and changing a repository's visibility does not add live data to a committed image.

Do not put access tokens in badge URLs. Public badge services cannot read private repository statistics without an appropriate authenticated backend.

Native ShieldCN badge groups do not reproduce the per-badge gradient styling and individual link destinations of these assets. Do not substitute a group and assume those features are preserved.

## Compatibility

The older files directly inside `Badges/` remain available for existing README links. New static navigation/profile integrations should use `Badges/ShieldCN/`. Retire an older file only after its known consumers have moved.
