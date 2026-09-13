# TSMP Codec Luma4

TSMP の標準 codec パッケージです。Luma4 は初回セットアップやストリーム経路の診断に使う基準 codec として推奨されます。

## 要件

- TSMP Core: https://github.com/kibalab/TSMP-Core
- Unity 2022.3
- `com.kibalab.tsmp.core` 0.3.0-beta.2 以降 (UPM 依存バージョン: 0.3.0-beta.2)
- VRChat ワールドで使用する場合のみ VRChat Worlds SDK 3.9.0 以降が必要

## 使い方

TSMP Core と一緒にこのパッケージをインストールし、Core の `Samples/TSMPController.prefab` をシーンに配置します。その後、`TSMPSetup` の Codec タブで `Luma4` を選択し、`Apply Setup` を実行します。

通常の Unity では、UPM の **Add package from disk** で Core 0.3.0-beta.2 とこのパッケージをインストールできます。VRCSDK/UdonSharp は不要です。両環境で同じ Controller プレハブを使い、コンポーネントとバインディングは自動準備されます。

## リリース状態

Luma4 0.0.4-beta.1 は TSMP Core 0.3.0-beta.2 向けのリリース候補です。Core を先に公開する必要があり、この候補はまだ VPM に配布していません。

## 準備 API の互換性

このソースには、現在リリース候補の Core 0.3.0-beta.2 が必要です。Core 0.2.0 と 0.3.0-beta.1 には `PrepareDecode` がなく、準備マテリアルを未設定にしてもコンパイルできません。このコーデックより先に対応 Core を公開・インストールしてください。準備マテリアル不足時の従来シェーダーへの fallback は、コンパイル後にのみ機能します。

UPM にはバージョン文字列、VPM には範囲を指定します。ローカル/ディスクまたは Git インストールでは、プロジェクトの依存関係に対応 Core も直接指定します。パッケージのメタデータだけでは UPM は GitHub から Core を取得しません。VPM ベータは公開後に試験版表示を有効にし、対応バージョンを選んでください。
