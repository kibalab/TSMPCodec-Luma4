# TSMP Codec Luma4

TSMP の標準 codec パッケージです。Luma4 は初回セットアップやストリーム経路の診断に使う基準 codec として推奨されます。

## 要件

- TSMP Core: https://github.com/kibalab/TSMP-Core
- Unity 2022.3
- `com.kibalab.tsmp.core` 1.0.0 以降 (UPM 依存バージョン: 1.0.0)
- VRChat ワールドで使用する場合のみ VRChat Worlds SDK 3.9.0 以降が必要

## 使い方

TSMP Core と一緒にこのパッケージをインストールし、Core の `Samples/TSMPController.prefab` をシーンに配置します。その後、`TSMPSetup` の Codec タブで `Luma4` を選択します。設定は自動適用されます。

通常の Unity では、UPM の **Add package from disk** で Core 1.0.0 とこのパッケージをインストールできます。VRCSDK/UdonSharp は不要です。両環境で同じ Controller プレハブを使い、コンポーネントとバインディングは自動準備されます。

## リリース状態

Luma4 1.0.0 は前回の正式版 0.0.3 以降のベータ変更をすべて統合した正式リリースです。Core 1.0.0 を先にインストールしてください。VCC で試験版表示を有効にする必要はありません。

## 準備 API の互換性

このリリースには Core 1.0.0 とそのコーデック準備・出力 API が必要です。コーデックをインストールする前に Core を更新してください。準備マテリアルがない場合は従来のシェーダー経路を使用できますが、互換性のない Core API を補うことはできません。

UPM は Core 1.0.0、VPM は Core >=1.0.0 を指定します。ローカル/ディスクまたは Git インストールでは、プロジェクトの依存関係に Core も直接指定してください。パッケージのメタデータだけでは UPM は GitHub から Core を取得しません。VRChat Worlds SDK は VPM のみの依存関係です。
