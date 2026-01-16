[English](README.md) | [日本語](README.ja.md)

# .NET 8 vs .NET 9 Comparison (Breaking Changes Focus)

This document compares **.NET 8** and **.NET 9** from a practical desktop/WPF developer perspective,
focusing on *breaking changes* and *real-world migration impact*.

---

## Toolchain / SDK

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| Visual Studio requirement | VS 17.8+ | **VS 17.12+ required** | **A** |
| MSBuild compatibility | Older OK | Old versions fail to load SDK | **A** |
| Target framework | net8.0 / net8.0-windows | net9.0 / net9.0-windows | C |

---

## Serialization / Security

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| BinaryFormatter | Obsolete but works | **Always throws exception** | **A** |
| Legacy binary compatibility | Still possible | **Completely removed** | **A** |
| Recommended alternatives | System.Text.Json etc. | Same | – |

---

## WPF / XAML

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| XmlNamespaceMaps type | string-based | **Hashtable-based** | **A** |
| XAML parser behavior | Stable | More strict / safer | B |
| UI Automation | Changed in 7/8 | No new breaking changes | C |

---

## Networking / Logging

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| HttpClient header logging | Plain text | **Masked (*) by default** | B |
| Redaction configuration | Optional | **Explicit allow/deny needed** | B |
| Security posture | Weaker | **Stronger by default** | – |

---

## Core Library / APIs

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| String.Trim(ReadOnlySpan<char>) | Preview existed | **Removed in GA** | B |
| TimeSpan.From* overloads | double only | **int overloads added** | C |
| RuntimeHelpers.GetSubArray | Old behavior | Optimized behavior | C |

---

## Compression / IO

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| Zip UTF-8 flag | Sometimes ignored | **Respected** | B |
| Zip entry names | Environment dependent | More spec-compliant | B |

---

## Environment Variables / OS

| Item | .NET 8 | .NET 9 | Impact |
|---|---|---|---|
| Empty environment variable | Treated like unset | **Distinguished** | B |
| Windows API integration | Stable | No change | C |

---

## Summary

### Definitely breaking (must fix)
- BinaryFormatter
- Visual Studio / CI toolchain
- WPF XmlNamespaceMaps

### Depends on usage
- HttpClient logging
- Zip handling
- Preview-only APIs

### Mostly safe
- WPF UI / DataGrid
- async / Task
- Virtualization

---

*Prepared for practical migration planning (WPF / Desktop focus).*
