# Joker MMO

一款基于 Unity 的实时多人在线动作 RPG，采用服务器权威架构与客户端预测回滚机制，实现流畅的低延迟战斗体验。

## 功能特性

### 核心玩法
- **实时战斗**：玩家攻击、受击、跳跃，支持武器特效与击打音效
- **怪物 AI**：基于 NavMesh 的怪物寻路、攻击、死亡状态机
- **药剂系统**：使用药水回血，实时同步至服务器
- **背包与道具**：武器、材料、消耗品、可堆叠物品，背包版本号保证客户端与服务端一致性
- **商人与 NPC**：对话系统、商店购买、任务/奖励系统
- **合成系统**：道具配方合成

### 网络架构
- **服务器权威**：所有游戏状态由服务端最终裁定
- **客户端预测 + 回滚**：客户端预先执行本地输入，服务端下发快照后自动对齐，消除操作延迟感
- **AOI（兴趣区域）**：服务端按区域过滤实体可见性，降低无关网络流量
- **自定义消息系统**：基于 Unity Netcode `CustomMessagingManager` 构建高效通信层

## 技术栈

| 技术 | 版本 / 说明 |
|------|------------|
| Unity | 2022.3.55f1c1 LTS |
| Unity Netcode for GameObjects | 1.11.0 |
| HybridCLR | 热更新支持（AOT + 热更 DLL 动态加载） |
| Addressables | 1.22.3，资源管理与内容分发 |
| Universal Render Pipeline (URP) | 14.0.11 |
| Cinemachine | 2.10.5，第三人称摄像机 |
| NavMesh AI | 怪物 / NPC 寻路 |
| JKFrame | 自定义 UI / 资源框架 |
| Toon Shader | 卡通渲染风格 |

## 项目结构

```
Assets/Scripts/
├── Common/         # 客户端与服务端共享逻辑（协议、数据结构）
├── Server/         # 服务端权威逻辑（PlayerServerController, MonsterServerController, AOI）
├── HotUpdate/      # 热更新模块（客户端控制器、UI、预测系统）
└── AOT/            # 始终编译的基础模块
```

- 客户端逻辑通过 **Partial Class** 拆分为独立文件（LoginSystem、ItemSystem、PredictionSystem 等）
- 角色行为采用**状态机**驱动（Idle / Attack / Hit / Die）
- 攻击配置、特效、伤害值均由 **ScriptableObject** 管理

## 快速开始

> 需要 Unity 2022.3.55f1c1 及 HybridCLR 环境。

1. 克隆仓库
   ```bash
   git clone https://github.com/Yuya1736/UnityMMOGame.git
   ```
2. 用 Unity Hub 打开项目
3. 执行菜单 `HybridCLR > Generate > All` 生成 AOT 元数据
4. 先启动 Server 场景，再启动 Client 场景进行本地测试

## 近期进展

- 客户端预测回滚基础架构完成，修复三处回滚缺陷
- 怪物系统、玩家受击、药剂回血全部完成
- 攻击命中逻辑、跳跃音效与特效完善

## License

本项目仅供学习与参考，暂未设置开源协议。
