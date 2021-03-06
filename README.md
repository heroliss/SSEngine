# SSEngine

## 1.简介
这是一个刚刚建立的游戏引擎，后面还需要不停的完善。有一些会影响到全局方案设计的基础模块已经确定，比如热更新方案。但还很多功能模块仍在调研一个最佳的解决方案。
当我刚开始建立这个项目的时候，就确立一个指定思想：尽量使用最先进的技术，面向未来而不是固守过去。当前该引擎还在非常初级阶段，但是先进的热更新方案和资源管理方案，使之必定会发展为高效易用的引擎。

## 2.解决方案(范围不限于该引擎，包含整个项目开发流程和最佳实践指导)

该引擎倾向于采用目前市面上最先进的技术作为各个模块的解决方案，若今后出现更好的解决方案，可以考虑替换，因此

随着项目的开发，以下方案内容可能会变化，并不断完善。

- 热更新方案：[Huatuo](https://focus-creative-games.github.io/huatuo/index)。Huatuo是一个在2022年4月才刚刚公布的热更新解决方案。官方文档中说到：huatuo是一个特性完整、零成本、高性能、低内存的近乎完美的Unity全平台原生c#热更方案。
- 资源管理方案：Addressables。Addressables是Unity官方最新的资源管理方案。Addressables可以自动管理资源依赖，方便加载，引用计数等很多好用的特性，编辑器工具也很全面，方便ab包管理，性能监控等各种开发需求。
- 异步方案：UniTask。取代协程和传统Task。无GC，提供完善的异步功能，完美适配unity传统的协程API和多种第三方插件。（可以考虑同时使用某种XAction链式编程技术）
- 简单动画方案：Dotween。高效易用，在一些简单动画上面，应全部使用Dotween取代Unity的传统帧动画Animation。
- 3D动画方案：Playable。这是Unity最新的动画状态控制系统，可以取代Unity传统的动画控制器。
- 特效方案：Visual Effect Graph。这是Unity最新的粒子特效系统，可以取代Unity传统的ParticleSystem。
- 程序集管理方案：使用Unity内置的AssmblyDefine，将项目从一开始就合理划分程序集，原则上Assembly-CSharp程序集中将不包含代码。
- 文字方案：使用textmeshpro，玩家名字和聊天内容因为多语言问题可能需要用text（待定）
- 多语言方案：可以考虑使用Unity官方提供的多语言方案，功能强大，但使用上比较复杂，还需根据项目实际情况考虑。如果是简单的文本图片替换，可以考虑直接使用Excel内容替换。
- 图形方案：对于手游项目使用URP，使用线性空间，使用ShaderGraph。(图形方面不懂，待完善)
- UI框架：调研了市面上不少的UI框架，目前还没确定要用哪个UI框架，可能是因为UI框架需要比较灵活的去适应当前正在开发项目的具体需求。上个项目我们自研的UI框架足够优秀，并且可以完全自主把控，因此可以考虑继续完善自研UI框架。
- UI组件扩展：UIExtensions。提供了很多UI扩展组件，但很多用不到，不一定用，可以考虑摘取其中有用的组件使用，并自研扩展一些UI组件。
- 版本控制方案：Git。Git作为一个常用且优秀的版本控制和代码管理方案，对于美术来说却似乎不是很友好，可能是因为有一定的使用门槛，因此有些项目组会使用SVN取代Git。但个人认为Git还是更好一点，对于非程序人员使用Git有门槛的问题，可以考虑写一套Git使用流程规范，来指导成员高效合理的使用Git。（Unity提供的内置版本管理也可以考虑）
- 配置表方案：实践证明，对于整个项目来说，Excel依然是目前最佳的配置表解决方案。程序方面可以考虑Luban插件读取配置表。
- 网络方案：网络部分没有深入调研。目前来看数据协议使用Proto3应该是最佳方案。程序部分未知。(待完善)
- Json方案：使用Huatuo完美支持并推荐的json库：[CatJson](https://github.com/CatImmortal/CatJson)
- 代码框架：可以考虑MVC结构，简单易用。可以考虑直接使用QFramework的部分功能，或自研代码框架。
  - 个人认为代码框架中比较重要必须要有的部分有：
    - 可观察对象的支持
    - 可自动释放方便管理生命周期的事件系统
    - 使用命令模式分离视图和控制器
  - 可以考虑锦上添花的部分：
    - 数据可视化支持（方便调试和模块开发）
    - 统一日志管理替代Debug.Log
    - 依赖注入

## 3.踩坑总结(一些规范和注意事项)：

随着项目的开发，以下内容可能会发生变化，并不断完善。

- 传统粒子系统在UI上显示需要用Canvas管理层级，不能使用UIParticleSystem组件，因为性能消耗太大
- Addressables使用规范：
  - 预制体应该由Addressables.InstantiateAsync实例化，并由Addressables.ReleaseInstance销毁，来替代Unity原始的Instantiate和Destory方法。
  - 预制体若要提前缓冲加载，应由Addressables.LoadAssetAsync<GameObject>预加载，并在生命周期结束时由Addressables.Release释放缓存。
  - 使用ComponentReference实例化含有特定组件的预制体，资产引用，自定义组件引用，精灵引用
- Git规范：
  - 考虑启用大文件技术支持
  - 设置Git参数，使上传时行尾自动改为/n
  - 合理使用子模块，将美术资源和程序代码分离
- Huotuo使用规范：
  - 必须IL2CPP
  - 禁用Incremental GC (暂时)
- 工程：
  - 善用场景，场景的优势即可自动释放资源
  - 善用Prefab变体，可以使设计复用度更高。并且自研的UI框架对预制体变体有很好的支持。
  - 按模块由大到小划分目录结构，然后按资源类型划分，同时要开发者和美术的分目录管理。
- 图形（不太懂随便写写，欢迎补充）：
  - 使用URP
  - 使用线性空间
  - 使用基于物理着色的着色器（哪怕是卡通风格的渲染）
  - 使用GPU实例化
  - 使用Material PropertyBlock
- 代码：
  - 合理使用命名空间。建议使用与目录路径相同的命名空间
  - 高性能部分可考虑使用ECS架构
  - 尽量保证各模块可独立运行，比如在MVC框架中，合理组装数据、控制器、视图，就可以实现一个可独立运行的模块。这样可以大大降低代码耦合度，更容易处理bug，更容易重构代码及功能。

## 4.引擎说明
该引擎目前还在非常初期的阶段，仅初步实现了Huatuo和Addressables的接入，以及其他一些基础插件的接入，如UniTask、Dotween等，并全部使用Assmbly Define细分了程序集。随着项目的开发，该引擎和使用说明会一起完善。

目前可以实现简单的资源和代码热更新。热更部分完整支持AOT泛型，并且无需与主工程分开，方便项目管理。在Unity编辑器中开发热更代码与原生开发几乎无区别。

简单来说，更新流程是：
  - 修改热更目录中的资源或代码
  - 点击更新打包按钮
  - 更新的资源（包括热更dll资源）会自动上传到配置好的服务端
  - 重新启动App，游戏在开始时自动比对资源变化并预下载更新的资源，也可设置在某个资源用到时自动下载。

