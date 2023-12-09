# 2021 GStar Project

## 프로젝트 소개

- 학교에서 유니티 게임 제작을 위한 실무 경험과 협업 경험을 쌓기 위한 팀 프로젝트
- 프로젝트 인원 : 3명
  - reoul(김용범) : 팀장, 메인 프로그래머, 기획
  - Hanyeoleum : 아트
  - KangHaNul : 사운드

## 개발환경

- 플랫폼 : PC
- 게임엔진 : Unity 2019.4.11

## 프로젝트 기간

- 2021.09 ~ 2021.11

## 플레이 영상

- https://youtu.be/nGTS4jPmDTU

## 다양한 시도 및 구현

- `ScriptableObject`를 사용한 데이터 관리
- `커스텀에디터`를 활용해 팀원과 원활한 협업 추구
- `DOTween`을 사용한 애니메이션 처리
- `코루틴`을 사용해서 일관된 순서의 로직 구현 (yield return StartCoroutine() 사용)

## 문제와 해결법

- 보여지는 이펙트와 사운드 간의 시간차가 발생
  - 원인 : 이펙트가 빠르게 사라지는 것과 사운드의 처음과 중간 부분의 텀이 있어서 소리가 느리게 들림
  - 해결법 : 사운드를 먼저 출력하고 이펙트 오브젝트를 일정 시간 딜레이 시켜서 생성되게 구현
https://github.com/reoul/Card-Labyrinthos/blob/61f79e6eb3b8f8e58f20a8379fe1e383ec641c0e/Assets/Scripts/EffectManager.cs#L52-L69

<br>

- 씬을 이동할 때 페이드아웃, 페이드인 효과를 구현했는데 각 구간마다 순서를 지켜야 하는 로직같은 경우 코드가 너무 복잡해짐
  - 해결법 : 코루틴에서 yield return StartCoroutine() 사용하면 해당 코루틴이 다 끝나야 다음 코드로 진행이 되서 FadeOut, FadeIn 전용 코루틴을 따로 만듬
https://github.com/reoul/Old-Project-Code/blob/main/PC%20카드%20로그라이크%20게임/Scripts/FadeManager.cs#L29-L77

https://github.com/reoul/Old-Project-Code/blob/5e16f95fe9da01f219ce1a74815a413da1945a87/PC%20%EC%B9%B4%EB%93%9C%20%EB%A1%9C%EA%B7%B8%EB%9D%BC%EC%9D%B4%ED%81%AC%20%EA%B2%8C%EC%9E%84/Scripts/FadeManager.cs#L79C6-L83C6

https://github.com/reoul/Old-Project-Code/blob/5e16f95fe9da01f219ce1a74815a413da1945a87/PC%20%EC%B9%B4%EB%93%9C%20%EB%A1%9C%EA%B7%B8%EB%9D%BC%EC%9D%B4%ED%81%AC%20%EA%B2%8C%EC%9E%84/Scripts/FadeManager.cs#L85C6-L94

## 코드 부분

- 카드 이동 코드
https://github.com/reoul/Card-Labyrinthos/blob/038e387c0f28573b9d040069317f82e22d048267/Assets/Scripts/Card.cs#L45-L63

<br>

- 전투 전에 오브젝트 풀링으로 카드 생성
https://github.com/reoul/Card-Labyrinthos/blob/61f79e6eb3b8f8e58f20a8379fe1e383ec641c0e/Assets/Scripts/CardManager.cs#L107-L143

<br>

- 스테이지 생성 코드
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/StageManager.cs#L33-L68

<br>

- 전투 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/61f79e6eb3b8f8e58f20a8379fe1e383ec641c0e/Assets/Scripts/TurnManager.cs#L42-L105
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/TurnManager.cs#L147-L178

<br>

- 몬스터 턴 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/cc590a333c67e063c64084533e0abce7056faa47/Assets/Scripts/Enemy.cs#L41-L81
https://github.com/reoul/Card-Labyrinthos/blob/cc590a333c67e063c64084533e0abce7056faa47/Assets/Scripts/Enemy.cs#L83-L99

<br>

- 카드 랜더링 순서 조정
https://github.com/reoul/Card-Labyrinthos/blob/61f79e6eb3b8f8e58f20a8379fe1e383ec641c0e/Assets/Scripts/CardManager.cs#L254-L263

<br>

- 카드 위치 정렬 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/61f79e6eb3b8f8e58f20a8379fe1e383ec641c0e/Assets/Scripts/CardManager.cs#L265-L276
https://github.com/reoul/Card-Labyrinthos/blob/d8910f0510c38779cecd405204af4be080800ea5/Assets/Scripts/CardManager.cs#L293-L345

<br>

- 카드 사용 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/5fd515b0914611784bd7042ef4e92cc8b3d6152d/Assets/Scripts/CardManager.cs#L376-L449
https://github.com/reoul/Card-Labyrinthos/blob/5fd515b0914611784bd7042ef4e92cc8b3d6152d/Assets/Scripts/CardManager.cs#L451-L463

<br>

- 스킬북
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/SkillBookPage.cs#L38-L104
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/SkillManager.cs#L80-L124
https://github.com/reoul/Card-Labyrinthos/blob/61f79e6eb3b8f8e58f20a8379fe1e383ec641c0e/Assets/Scripts/SkillManager.cs#L154-L184

<br>

- 디버프 적용 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/5fe366e22b49981e805e4b62278227b211218bd8/Assets/Scripts/DebuffManager.cs#L45-L93

<br>

- 이펙트 생성 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/e67f1179d692045149a59dc365ade75177e9e6ad/Assets/Scripts/EffectManager.cs#L52-L69
https://github.com/reoul/Card-Labyrinthos/blob/11df0b1d75a504fff39931b5484158f95ef23325/Assets/Scripts/EffectManager.cs#L71-L82

<br>

- 이벤트 씬 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/a3fa59e5b2ed6ff6d7d1e3471cbc9a19fdfa231e/Assets/Scripts/Event.cs#L13-L33
https://github.com/reoul/Card-Labyrinthos/blob/a3fa59e5b2ed6ff6d7d1e3471cbc9a19fdfa231e/Assets/Scripts/Event.cs#L35-L46

<br>

- 이벤트 보상 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/cbae3483eecacf95dba6e169cc973864aab313ef/Assets/Scripts/EventManager.cs#L57-L96
https://github.com/reoul/Card-Labyrinthos/blob/756ca5dd2a0109bfff0552fb4411ea931344290d/Assets/Scripts/EventManager.cs#L139-L158

<br>

- 커스텀 에디터
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/Field.cs#L8-L103

<br>

- ScriptableObject
https://github.com/reoul/Card-Labyrinthos/blob/0d558398f6181d2c84248764f11fbd77c00842c5/Assets/Scripts/SO/MonsterSO.cs#L67-L88

<br>

- 튜토리얼 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/Ghost.cs#L8-L18
https://github.com/reoul/Card-Labyrinthos/blob/e5387ad47dd73793b76fab5593da1ac0fb88db23/Assets/Scripts/MapManager.cs#L310-L509

<br>

- 튜토리얼에 특정 부분을 강조시킬때 사용되는 화살표 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/ae8c7341e9c6ba10d279cfb72f9886ffdeaf06ec/Assets/Scripts/ArrowManager.cs#L25-L83

<br>

- 보상 관련 코드
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/Reward.cs#L64-L87
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/RewardManager.cs#L266-L339
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/RewardManager.cs#L61-L136
https://github.com/reoul/Card-Labyrinthos/blob/cbff110a6c3d3b18b4d418b6795d57534e289f45/Assets/Scripts/TurnManager.cs#L188-L206

