# Appium Automation Skill - Slide Content

## Slide Title
**Appium Automation Skill** - Cross-Platform UI Testing for Copilot CLI

---

## Key Message
Automate .NET MAUI apps on iOS, Android, and Mac Catalyst directly from Copilot CLI

---

## Bullet Points

• **Cross-Platform** - iOS Simulator, Android Emulator, Mac Catalyst
• **Rich Actions** - tap, type, swipe, scroll, drag, slider manipulation
• **Element Discovery** - `--list-elements`, `--list-buttons`, `--find-text`
• **Assertions** - `--expect` for UI testing with pass/fail exit codes
• **Session Caching** - 10x faster subsequent calls with `--reuse-session`
• **AutomationId Targeting** - Reliable element identification via XAML

---

## CLI Examples

```bash
# Discover UI elements
python automate.py --platform ios --app-id com.example.app --list-elements

# Automate a flow
python automate.py --platform ios --app-id com.example.app \
  --type SubtotalEntry 100 \
  --set-slider TipSlider 20 \
  --expect TipAmountValue "€ 20,00"

# Take screenshot after action
python automate.py --platform android --app-id com.example.app \
  --tap SubmitButton --wait 2 --screenshot result.png
```

---

## How Copilot Uses It

1. **Reproducing Issues** - "Tap the login button and show me what happens"
2. **Validating Fixes** - "Verify the tip calculator shows €15 for 100 subtotal at 15%"
3. **Exploring UI** - "List all buttons on the current screen"
4. **Testing PRs** - Automated UI verification before merge

---

## Setup Requirements

```bash
# Install Appium
npm install -g appium
appium driver install xcuitest      # iOS
appium driver install uiautomator2  # Android

# Install Python client
pip install Appium-Python-Client selenium
```

---

## Speaker Notes

The Appium Automation Skill enables Copilot CLI to interact with real mobile apps running on simulators/emulators. This bridges the gap between code changes and UI verification - Copilot can now not just write code, but verify it works on device.

Key differentiator: Session caching. Each Appium session takes 10-50s to start on iOS. With `--keep-session` and `--reuse-session`, subsequent operations take ~2s each.

For .NET MAUI apps, the skill leverages AutomationId for reliable element targeting. The AutomationId maps to accessibility identifiers on each platform (iOS: name, Android: resource-id, Mac: identifier).

---

# Demo Outline

## Demo: Tip Calculator Automation

### Setup (Before Demo)
1. Boot iOS Simulator: `xcrun simctl boot "iPhone 16 Pro"`
2. Start Appium: `appium --relaxed-security &`
3. Build and install the sample app
4. Have the app open on simulator

### Demo Flow (3-4 minutes)

#### Part 1: Element Discovery (30s)
```bash
# Show Copilot discovering the UI
python .github/skills/appium-automation/scripts/automate.py \
  --platform ios \
  --app-id com.companyname.mauixamlcsharpsample \
  --list-elements
```
**Talking point**: "Copilot can explore the app's UI structure to understand what's available"

#### Part 2: Simple Interaction (30s)
```bash
# Type into the Name field
python .github/skills/appium-automation/scripts/automate.py \
  --platform ios \
  --app-id com.companyname.mauixamlcsharpsample \
  --type NameEntry "Build 2026 Attendee" \
  --wait 1 \
  --screenshot demo-name.png
```
**Talking point**: "Natural language to UI action - no manual clicking needed"

#### Part 3: Chained Actions with Verification (1m)
```bash
# Complete flow: set values, verify calculation
python .github/skills/appium-automation/scripts/automate.py \
  --platform ios \
  --app-id com.companyname.tipcalc \
  --tap SubtotalEntry \
  --type SubtotalEntry "100" \
  --set-slider TipSlider 15 \
  --dismiss-keyboard \
  --wait 1 \
  --expect TipAmountValue "€ 15" \
  --expect TotalValue "€ 115" \
  --screenshot tip-calc-result.png
```
**Talking point**: "One command: input values, verify calculations, capture evidence"

#### Part 4: Session Caching Demo (1m)
```bash
# First call - creates session (~15s)
time python automate.py --platform ios --app-id com.example \
  --keep-session --tap Field1

# Second call - reuses session (~2s)
time python automate.py --platform ios --app-id com.example \
  --reuse-session --keep-session --type Field1 "fast!"

# Cleanup
python automate.py --end-session
```
**Talking point**: "Session caching makes iterative testing 10x faster"

#### Part 5: Assertion Failure (30s)
```bash
# Show what happens when assertion fails
python .github/skills/appium-automation/scripts/automate.py \
  --platform ios \
  --app-id com.companyname.tipcalc \
  --expect TipAmountValue "€ 999"
# Output: ✗ expect 'TipAmountValue' contains '€ 999': FAIL (actual: € 15,00)
# Exit code: 1
```
**Talking point**: "Clear pass/fail for CI integration - exit code 1 on failure"

### Key Takeaways to Emphasize

1. **No manual clicking** - Copilot drives the UI programmatically
2. **Assertions = Tests** - `--expect` enables automated verification
3. **Session caching** - Critical for interactive/iterative use
4. **Cross-platform** - Same commands work on iOS, Android, Mac Catalyst
5. **MAUI-native** - Uses AutomationId for reliable targeting

---

## Backup Demo Commands

If the sample app isn't available, use these generic exploration commands:

```bash
# List available devices
python automate.py --list-devices

# Boot a simulator
python automate.py --boot-simulator "iPhone 16 Pro"

# Start Appium
python automate.py --start-appium

# Generic exploration of any installed app
python automate.py --platform ios --app-id <any-bundle-id> \
  --list-buttons
```
