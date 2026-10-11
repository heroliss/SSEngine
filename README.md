# SSEngine

Unity 游戏引擎早期实验项目，重点验证 Huatuo 热更新、Addressables 资源管理和项目启动流程。

## 当前状态

这是一个已经冻结的早期工程骨架，不是可直接交付的完整游戏。仓库最后一次 push 为 2022-06-17。项目依赖旧版 Unity、外部 Huatuo registry、资源服务器和未完成的构建流程。

## 环境

- Unity 2020.3.33f1
- Huatuo 0.1.7
- Addressables 1.18.19
- URP 10.8.1
- TextMeshPro 3.0.6

## 启动方式

1. 使用 Unity Hub 安装 Unity 2020.3.33f1。
2. 打开仓库根目录，等待 Package Manager 解析依赖。
3. 打开 `Assets/Bootstrap/Scene/BootstrapScene.unity`。
4. 在 Editor 中运行场景，检查启动和预下载流程。

热更新和资源构建需要配置 Huatuo registry、资源服务器和项目 Editor 构建菜单；当前未完成端到端复现。

## 主要结构

- `Assets/Bootstrap`：启动场景和启动脚本。
- `Assets/HotUpdate`：热更新程序集、Hall/Game1/Game2 场景和脚本。
- `Assets/Editor`：Huatuo 生成和构建流程。
- `Packages`：Unity 包和项目依赖。

## 已知限制

依赖旧版 Unity 和外部服务端配置；没有成品游戏或稳定发布包。仓库未声明统一许可证，第三方组件按各自许可证为准。

