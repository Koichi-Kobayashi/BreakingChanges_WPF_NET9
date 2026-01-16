# .NET 9 Breaking Changes Lab (WPF/Desktop focused)

このリポジトリは、**.NET 9 の破壊的変更**を「プロジェクト単位」で再現できるようにした検証用ソリューションです。

- **Broken**: 破壊的変更により *例外/挙動差/ビルド差* が出る版
- **Fixed**: 推奨される修正版

> ⚠️ WPF プロジェクトは Windows / Visual Studio 上で実行してください（この環境ではビルドしていません）。

## 前提
- Visual Studio 2022 **17.12+**（`net9.0` ターゲットのため）
- .NET SDK 9.x

## 収録プロジェクト

### 01) BinaryFormatter (破壊的: 常に例外)
- `BC901_BinaryFormatter_Net8` : .NET 8 では（警告は出るが）動作する例
- `BC901_BinaryFormatter_Net9_Broken` : .NET 9 では **必ず例外**になる再現
- `BC901_BinaryFormatter_Net9_Fixed` : `System.Text.Json` への移行例

### 02) HttpClientFactory ログ (ヘッダー値の既定マスク)
- `BC902_HttpClientLogging_Net8`
- `BC902_HttpClientLogging_Net9`

> 実行後、コンソール出力のヘッダー値が .NET 8 と .NET 9 で違うことを確認してください。

### 03) WPF: GetXmlNamespaceMaps type change
- `BC903_Wpf_XmlNamespaceMaps_Net8` : **起動時に InvalidCastException が発生**する再現
- `BC903_Wpf_XmlNamespaceMaps_Net9` : .NET 9 の新挙動（例外が出ない）を確認

## 使い方
1. `DotNet9BreakingChangesLab.sln` を開く
2. 目的のプロジェクトを **Startup Project** に設定
3. 実行

## 参考 (一次情報)
- WPF `GetXmlNamespaceMaps` type change (.NET 9)
- `BinaryFormatter` disabled (runtime)
- HttpClientFactory logging redaction (headers)

