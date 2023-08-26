# 2022 PlayX4 Project

## 프로젝트 소개

- 학교에서 유니티 게임 제작을 위한 실무 경험과 협업 경험을 쌓기 위한 팀 프로젝트
- 프로젝트 인원 : 4명
  - reoul(김용범) : 팀장, 메인 프로그래머, 기획
  - GiSeok : 보조 프로그래머
  - bykiwi99 : 아트
  - WhiteWeb0707 : 사운드

## 개발환경

- 플랫폼 : Steam VR(VIVE)
- 게임엔진 : Unity 2019.4.36

## 프로젝트 기간

- 2022.03 ~ 2022.05 PlayX4 제작 기간
- 2022.06 ~ 2022.07 인디크래프트 준비

## 플레이 영상

- https://youtu.be/Z7LiuODtMRI

## 프로젝트 담당 정리

### reoul(김용범)

#### 다양한 시도 및 구현

- `Xml` 문서로 외부에서 세팅값 설정할 수 있게 구현
- `커스텀에디터`로 작업에 도움되는 툴 제작
- `리플랙션`으로 코드 간결화
- `인터페이스`와 `추상 클래스`의 도입으로 확장성 고려

#### 문제와 해결법

- 진동을 `Update`에서 호출했는데 갑자기 한쪽이 진동 안되고, 일정 이상 진동세기가 올라가면 소리 커지면서 일정한 진동을 유지 못했음
  - 해결법 : 진동을 매 프레임 호출하지 않고 `0.1` 초마다 호출하게 구현함
https://github.com/reoul/2022_HS_PLAYX4/blob/e1e5c2e19fc87035143630836585b4989ac718c9/Assets/KYB/Scripts/VRController.cs#L102-L118

<br>

- 이 게임은 실제 사람이 몸을 움직여 적의 공격을 피해야 하는데 VR을 장착하고 실제 움직여보니 실제보다 덜 움직이는 것처럼 보였음
  - 해결법 : `[CameraRig]` 오브젝트의 `Scale`을 `(1.3f, 1f, 1.3f)`로 설정하니 VR을 장착하고 움직여도 이질감이 안생김

<br>

- 나무와 나뭇잎에 `DissolveMat`을 적용하니 프레임이 5 이하로 떨어졌음.
  - 원인 : 나뭇잎이 적어도 몇 천개가 있을거고 그 많은 나뭇잎을 일일이 연산해서 프레임이 떨어지는 것으로 보임
  - 해결법 : 나뭇잎 하나하나 계산하는 것을 `DissolveMatAll` 스크립트를 작성하여 `BoxCollider` 하나와 스크립트 한개가 나무 하나를 담당하게 하여 성능을 높임 
  
<br>

- 나무 프리팹에도 `DissolveMat`을 넣었는데 나무 하나에 나무잎이 너무 많아 시간이 오래 걸린다
  - 해결법 : `커스텀에디터`로 작업 툴을 제작
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/ChangeMatTool.cs#L9-L80

#### 코드 부분

- `인터페이스`를 통해 확장성 고려
https://github.com/reoul/2022_HS_PLAYX4/blob/059f0eed13b366c9a12eac755f86bde2d0ecf765/Assets/KYB/Scripts/IHitable.cs#L1-L7
https://github.com/reoul/2022_HS_PLAYX4/blob/059f0eed13b366c9a12eac755f86bde2d0ecf765/Assets/KYB/Scripts/ArrowManager.cs#L13-L21

<br>

- `커스텀에디터`를 활용하여 작업 툴 제작
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/ChangeMatTool.cs#L9-L80

<br>

- Xml 파싱
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/DataManager.cs#L32-L85

<br>

- `리플랙션`으로 코드 간결화
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/DataManager.cs#L87-L98

<br>

- Dissolve 관련 코드
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/DissolveMat.cs#L94-L115
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/DissolveMat.cs#L144-L157
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/DissolveMat.cs#L182-L187

<br>

- 나레이션 관련 코드
https://github.com/reoul/2022_HS_PLAYX4/blob/4c8d94b80b6dfb5a258fe4775d3176fa05e2bb9d/Assets/KYB/Scripts/NarrationManager.cs#L105-L195

<br>

- 스테이지를 `abstract` 클래스로 선언, 하위 클래스가 재정의하는 방식으로 하여 확장성 고려
https://github.com/reoul/2022_HS_PLAYX4/blob/40a094440d2033f3126896445c3244ecbc53f19d/Assets/KYB/Scripts/Stage.cs#L40-L59
https://github.com/reoul/2022_HS_PLAYX4/blob/40a094440d2033f3126896445c3244ecbc53f19d/Assets/KYB/Scripts/Stage1.cs#L5-L29
https://github.com/reoul/2022_HS_PLAYX4/blob/40a094440d2033f3126896445c3244ecbc53f19d/Assets/KYB/Scripts/Ending.cs#L3-L26

<br>

- `VRController` 진동 관련 
https://github.com/reoul/2022_HS_PLAYX4/blob/e1e5c2e19fc87035143630836585b4989ac718c9/Assets/KYB/Scripts/VRController.cs#L102-L118

<br>

- `LineRenderer`를 통해 활 시위와 화살의 궤적을 표현
https://github.com/reoul/2022_HS_PLAYX4/blob/539db9cbe35c8c4a59f5820b07fad0584df7e648/Assets/KYB/Scripts/BowManager.cs#L47-L95

### GiSeok

- 보조 프로그래머

### bykiwi99

- 아트

### WhiteWeb0707

- 사운드
