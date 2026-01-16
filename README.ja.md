# .NET 8 vs .NET 9 比較表（破壊的変更・実務向け）

本ドキュメントは **.NET 8** と **.NET 9** を、  
**WPF / デスクトップアプリ開発者視点**で比較し、  
移行時に影響が出やすい *破壊的変更・挙動差* を整理したものです。

---

## ツールチェーン / SDK

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| Visual Studio 要件 | VS 17.8+ | **VS 17.12+ 必須** | **A** |
| MSBuild 互換性 | 旧版でも可 | 古いと SDK をロード不可 | **A** |
| ターゲットフレームワーク | net8.0 / net8.0-windows | net9.0 / net9.0-windows | C |

---

## シリアライズ / セキュリティ

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| BinaryFormatter | 非推奨だが動作 | **常に例外を送出** | **A** |
| 旧バイナリ互換 | まだ可能 | **完全に不可** | **A** |
| 推奨代替 | System.Text.Json 等 | 同左 | – |

---

## WPF / XAML

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| XmlNamespaceMaps の型 | string ベース | **Hashtable に変更** | **A** |
| XAML パーサ挙動 | 従来通り | より安全・厳密 | B |
| UI Automation | 7/8 で変更済 | 新たな破壊なし | C |

---

## Networking / ログ

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| HttpClient ヘッダーログ | 平文出力 | **既定で `*` マスク** | B |
| Redaction 設定 | 明示不要 | **明示指定が必要** | B |
| セキュリティ姿勢 | やや緩い | **安全側に強化** | – |

---

## Core Library / API

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| String.Trim(ReadOnlySpan<char>) | Preview に存在 | **GA で削除** | B |
| TimeSpan.From* | double のみ | **int オーバーロード追加** | C |
| RuntimeHelpers.GetSubArray | 従来挙動 | 最適化変更 | C |

---

## 圧縮 / IO

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| Zip UTF-8 フラグ | 無視される場合あり | **仕様通り尊重** | B |
| Zip エントリ名 | 環境依存 | より仕様準拠 | B |

---

## 環境変数 / OS 連携

| 項目 | .NET 8 | .NET 9 | 影響度 |
|---|---|---|---|
| 空文字の環境変数 | 未定義と同等 | **区別される** | B |
| Windows API 連携 | 安定 | 変更なし | C |

---

## まとめ

### 🔴 必ず修正が必要
- BinaryFormatter の使用
- Visual Studio / CI の更新
- WPF XmlNamespaceMaps 周り

### 🟡 実装次第で影響あり
- HttpClient ログ
- Zip（日本語ファイル名）
- Preview API 利用コード

### 🟢 ほぼ影響なし
- WPF UI / DataGrid
- async / Task
- 仮想化周り

---

*WPF / デスクトップアプリの .NET 9 移行判断用資料として作成*
