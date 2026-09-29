---
name: "attendance-review"
description: "勤怠管理コードの品質チェック"
agent: "attendance-expert"
model: "gpt-4o"
---

以下の観点で勤怠管理コードをレビューせよ：

- 勤怠モデル（Employee, Company, Attendance）の整合性
- 勤怠更新ロジックの妥当性（新規作成・更新）
- LINQ の適切な使用
- null チェックの適切性
- コードの可読性と保守性
- SOLID 原則に反していないか
