# TSMP Codec Luma4

TSMP の標準 codec パッケージです。Luma4 は初回セットアップやストリーム経路の診断に使う基準 codec として推奨されます。

## 要件

- TSMP Core: https://github.com/kibalab/TSMP-Core
- Unity 2022.3
- `com.kibalab.tsmp.core` 1.0.0 以降 (UPM 依存バージョン: 1.0.0)
- VRChat ワールドで使用する場合のみ VRChat Worlds SDK 3.9.0 以降が必要

## 使い方

TSMP Core と一緒にこのパッケージをインストールし、Core の `Samples/TSMPController.prefab` をシーンに配置します。その後、`TSMPSetup` の Codec タブで `Luma4` を選択します。設定は自動適用されます。

通常の Unity では、UPM の **Add package from disk** で Core 1.0.0 以降 とこのパッケージをインストールできます。VRCSDK/UdonSharp は不要です。両環境で同じ Controller プレハブを使い、コンポーネントとバインディングは自動準備されます。

## 受信側の設定

受信側の **Use Bilinear 4** は、対応する Core と組み合わせると、入力解像度と一致する 4×4 の領域を 4 回のフィルタ付きサンプルで平均します。他のサイズや旧 Core では従来どおりです。**Prefer Calibration Lut** は単一サンプルでも LUT の準備を試みます。初回受信時の停止を調べる場合に比較してください。追加の描画パスが必要なため既定ではオフです。どちらも送信フォーマットは変更しません。

## リリース状態

[Luma4 1.1.0](https://github.com/kibalab/TSMPCodec-Luma4/releases/tag/v1.1.0) は受信処理を改善した正式版です。Core 1.1.0 を推奨します。Core 1.0.0 も引き続き使用でき、送信フォーマットに変更はありません。

## 準備 API の互換性

このリリースには Core 1.0.0 以降 とそのコーデック準備・出力 API が必要です。コーデックをインストールする前に Core を更新してください。準備マテリアルがない場合は従来のシェーダー経路を使用できますが、互換性のない Core API を補うことはできません。

UPM は Core 1.0.0、VPM は Core >=1.0.0 を指定します。ローカル/ディスクまたは Git インストールでは、プロジェクトの依存関係に Core も直接指定してください。パッケージのメタデータだけでは UPM は GitHub から Core を取得しません。VRChat Worlds SDK は VPM のみの依存関係です。
