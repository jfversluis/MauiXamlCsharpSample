# XAML C# Expressions Demo

This sample app demonstrates the new **XAML C# Expressions** feature coming to .NET MAUI - allowing you to embed C# expressions directly in XAML property values.

> ⚠️ **Preview Warning**: This feature is experimental and planned for **.NET 11**. The API and syntax may change before release. This demo uses a custom preview build of .NET MAUI on .NET 10 and **should not be used in production**.

## What is XAML C# Expressions?

XAML C# Expressions is a new XAML Source Generator feature that lets you write C# code directly in your XAML, eliminating the need for many common converters and code-behind event handlers.

## Features Demonstrated

### 1. Simple Property Binding
```xml
<!-- NEW: Implicit syntax -->
<Label Text="{Name}" />

<!-- NEW: Explicit syntax (with = prefix) -->
<Label Text="{= Name}" />

<!-- OLD: Traditional binding -->
<Label Text="{Binding Name}" />
```

### 2. String Interpolation
```xml
<!-- NEW -->
<Label Text="{$'Hello, {Name}!'}" />

<!-- OLD -->
<Label Text="{Binding Name, StringFormat='Hello, {0}!'}" />
```

### 3. Calculations (Multi-Root Expressions)
```xml
<!-- NEW -->
<Label Text="{$'Total: ${Price * Quantity:F2}'}" />

<!-- OLD: Required a computed property or MultiBinding with converter -->
<Label Text="{Binding Total, StringFormat='Total: ${0:F2}'}" />
```

### 4. Boolean Negation
```xml
<!-- NEW -->
<Label IsVisible="{!IsHidden}" />

<!-- OLD: Required BooleanInvertConverter -->
<Label IsVisible="{Binding IsHidden, Converter={StaticResource BooleanInvertConverter}}" />
```

### 5. Boolean Expressions
```xml
<!-- NEW -->
<Button IsEnabled="{HasAccount &amp;&amp; AgreedToTerms}" />

<!-- OLD: Required MultiBinding with AllTrueConverter -->
<Button>
    <Button.IsEnabled>
        <MultiBinding Converter="{StaticResource AllTrueConverter}">
            <Binding Path="HasAccount" />
            <Binding Path="AgreedToTerms" />
        </MultiBinding>
    </Button.IsEnabled>
</Button>
```

### 6. Lambda Event Handlers
```xml
<!-- NEW -->
<Button Clicked="{(s, e) => this.OnCounterClicked()}" />

<!-- OLD -->
<Button Clicked="OnCounterClicked" />
```

### 7. Async Event Handlers
```xml
<!-- NEW -->
<Button Clicked="{async (s, e) => await SaveAsync()}" />

<!-- OLD: Required async void event handler in code-behind -->
<Button Clicked="OnSaveClicked" />
```

## Requirements

To enable XAML C# Expressions, add the following to your `.csproj`:

```xml
<PropertyGroup>
    <MauiXamlInflator>SourceGen</MauiXamlInflator>
</PropertyGroup>
```

You also need `x:DataType` set on your XAML page for the expression resolver to work:

```xml
<ContentPage x:DataType="local:MainViewModel">
```

## Syntax Reference

| Syntax | Description | Example |
|--------|-------------|---------|
| `{Property}` | Implicit binding to x:DataType | `{Name}` |
| `{= Property}` | Explicit expression syntax | `{= Name}` |
| `{$'...'}` | String interpolation | `{$'Hello {Name}'}` |
| `{!Bool}` | Boolean negation | `{!IsHidden}` |
| `{A && B}` | Boolean AND | `{HasAccount && AgreedToTerms}` |
| `{A \|\| B}` | Boolean OR | `{IsAdmin \|\| IsOwner}` |
| `{A * B}` | Arithmetic expressions | `{Price * Quantity}` |
| `{(s, e) => ...}` | Lambda event handler | `{(s, e) => Count++}` |
| `{async (s, e) => ...}` | Async lambda handler | `{async (s, e) => await LoadAsync()}` |
| `{this.Property}` | Explicit page member | `{this.Title}` |
| `{.Property}` | Explicit binding context | `{.Name}` |

## How It Works

The XAML Source Generator:
1. Parses expressions at compile time using Roslyn
2. Auto-detects whether identifiers belong to `this` (page) or `x:DataType` (ViewModel)
3. Generates strongly-typed bindings or direct property access
4. Creates lambda wrappers for event handlers

## Resources

- [PR #33693: XAML C# Expressions](https://github.com/dotnet/maui/pull/33693)
- [Full Specification](https://github.com/dotnet/maui/blob/dev/stdelc/xaml%2Bcode-clean/docs/specs/XamlCSharpExpressions.md)

## Disclaimer

This repository contains a **preview/experimental feature** built from a PR branch. It is:
- ❌ Not production ready
- ❌ Subject to breaking changes
- ❌ Using unofficial NuGet packages
- ✅ For demonstration and feedback purposes only

## License

This sample is provided as-is for educational purposes.
