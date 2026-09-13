[한국어](README.ko.md) | [English](README.en.md) | **日本語**

# TSMP Codec Luma4

Luma4 は TSMP の標準 codec です。豊富な色情報に依存せず、輝度ベースのシンボルで TSMP データを記録するため、セットアップが簡単で安定したデコード経路の基準 codec として使えます。

TSMP を初めて導入する場合やストリーム経路を確認する場合は、まず Luma4 を使用してください。

## 特徴

- TSMP 標準 codec
- 色への依存が少なく、デコード経路が単純
- VRChat カメラ キャプチャ、OBS、Spout などの映像経路で確認しやすい基準パターン
- `TSMPSetup` の Codec タブで自動検出

## 要件

- TSMP Core: https://github.com/kibalab/TSMP-Core
- Unity 2022.3
- `com.kibalab.tsmp.core` 0.3.0-beta.2 以降 (UPM 依存バージョン: 0.3.0-beta.2)
- VRChat ワールドで使用する場合のみ VRChat Worlds SDK 3.9.0 以降が必要

## インストール

VRChat Creator Companion で VPM リポジトリを追加します。

```text
https://vpm.kiba.red/
```

その後、`TSMP Core` と `TSMP Codec Luma4` をインストールします。

通常の Unity では、UPM の **Add package from disk** で Core 0.3.0-beta.2 とこのパッケージをインストールします。VRCSDK/UdonSharp は不要です。両環境で同じ Controller プレハブと自動セットアップを使用します。

## 使い方

1. Core パッケージの `Packages/com.kibalab.tsmp.core/Samples/TSMPController.prefab` をシーンに配置します。
2. `TSMPSetup` の Codec タブで `Refresh Codecs` を押します。
3. `Luma4` を選択します。
4. 入出力設定を確認します。コンポーネントとバインディングは自動準備され、`Apply Setup` で手動更新することもできます。

## リリース状態

Luma4 0.0.4-beta.1 は TSMP Core 0.3.0-beta.2 向けのリリース候補です。Core を先に公開する必要があり、この候補はまだ VPM に配布していません。

## ライセンス

MIT License. Copyright (c) 2026 KIBA_Labs.

## 準備 API の互換性

このソースには、現在リリース候補の Core 0.3.0-beta.2 が必要です。Core 0.2.0 と 0.3.0-beta.1 には `PrepareDecode` がなく、準備マテリアルを未設定にしてもコンパイルできません。このコーデックより先に対応 Core を公開・インストールしてください。準備マテリアル不足時の従来シェーダーへの fallback は、コンパイル後にのみ機能します。

UPM にはバージョン文字列、VPM には範囲を指定します。ローカル/ディスクまたは Git インストールでは、プロジェクトの依存関係に対応 Core も直接指定します。パッケージのメタデータだけでは UPM は GitHub から Core を取得しません。VPM ベータは公開後に試験版表示を有効にし、対応バージョンを選んでください。
