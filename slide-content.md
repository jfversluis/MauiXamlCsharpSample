# XAML C# Expressions - Slide Content

## Slide Title
**XAML C# Expressions** - Write C# Directly in XAML

---

## Key Message
Eliminate converters and code-behind with inline C# expressions in XAML

---

## Bullet Points

• **String Interpolation** - `{$'Hello, {Name}!'}` instead of StringFormat
• **Calculations** - `{$'Total: ${Price * Quantity:F2}'}` without computed properties  
• **Boolean Negation** - `{!IsHidden}` without BooleanInvertConverter
• **Boolean Logic** - `{HasAccount && AgreedToTerms}` without MultiBinding
• **Lambda Handlers** - `{(s, e) => Count++}` without code-behind methods
• **Async Lambdas** - `{async (s, e) => await SaveAsync()}` inline

---

## Before & After Code Example

### Before (Traditional XAML)
```xml
<Label Text="{Binding Name, StringFormat='Hello, {0}!'}" />
<Label IsVisible="{Binding IsHidden, 
       Converter={StaticResource BooleanInvertConverter}}" />
<Button IsEnabled="{Binding CanSubmit}" />  <!-- Requires ViewModel property -->
<Button Clicked="OnSaveClicked" />           <!-- Requires code-behind -->
```

### After (XAML C# Expressions)
```xml
<Label Text="{$'Hello, {Name}!'}" />
<Label IsVisible="{!IsHidden}" />
<Button IsEnabled="{HasAccount && AgreedToTerms}" />
<Button Clicked="{async (s, e) => await SaveAsync()}" />
```

---

## How to Enable

```xml
<PropertyGroup>
    <MauiXamlInflator>SourceGen</MauiXamlInflator>
</PropertyGroup>
```

Plus `x:DataType` on your page for IntelliSense & type safety

---

## Timeline
• 🔬 **Preview**: Available now in experimental builds
• 📅 **Target**: .NET 11 (November 2026)

---

## Speaker Notes

XAML C# Expressions is a XAML Source Generator feature that allows embedding C# code directly in XAML property values. This eliminates the need for:
- Value converters (BooleanInverter, AllTrue, etc.)
- Computed properties in ViewModels just for display formatting
- Code-behind event handlers for simple operations
- MultiBindings for combining values

The generator uses Roslyn to parse expressions at compile time and generates strongly-typed bindings. It auto-detects whether identifiers belong to the page (`this`) or the DataType (ViewModel).

Demo app: MauiXamlCsharpSample shows all 7 feature categories with before/after comparisons.
