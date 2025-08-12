# 3DSparta
Unity Sparta Bootcamp Chapter 5
Version 1.0.0


## 📖 목차
1. [프로젝트 소개](#-프로젝트-소개)
2. [주요기능](#-주요기능)
3. [개발기간](#-개발기간)
4. [기술스택](#-기술스택)
5. [프로젝트 파일 구조](#-프로젝트-파일-구조)
6. [Trouble Shooting](#️-trouble-shooting)

---

## 프로젝트 소개

게임성이 없는 기본적인 3D 플랫폼 입니다.

---

##  조작법

| 기능                           | 키                 | 조건
|--------------------------------|--------------------|-------------------|
| **이동**                       | `W`, `A`, `S`, `D` | 인벤토리 UI가 뜨지 않은 경우
| **점프**                       | `Space`            | 인벤토리 UI가 뜨지 않은 경우
| **아이템 들기**                | **좌클릭**         | IGrabable 오브젝트
| **아이템 줍기 / 상호작용**     | `E`                | IInteractable 오브젝트
| **인벤토리 열기**              | `Tab`              |
| **인벤토리 닫기**              | **Esc**            |

---

###  디버그용 단축키

| 기능                           | 키     | 조건
|--------------------------------|--------|---------------|
| 디버그 액션                     | `K`    | DEBUGMODE가 켜져있을 경우

---

##  주요기능

### 1. 인터페이스 친화적 구조화
  #### 1-1. <아이템>
  - Scriptable Object를 이용하여 아이템 분류
  - Interface 지향적 구조를 활용
    - Brige 패턴을 활용해서 필요한 Interface를 item에 넣어주는 설계 진행
    - Scriptable Object에 정보를 저장하고, 실제 GameObject에 인터페이스를 참조하여 작동함
    - IsUsable을 통하여 각 기능을 모듈화 해서 넣어주기 가능
      
  - Object Pooling 활용
    - 아이템이 소환되는 ItemPool에 아이템을 미리 제작 후 필요 시 Active 하는 방식 활용
      
  #### 1-2. <Entity, 플레이어>
  - 플레이어, 매니저, 컨트롤러, 인터렉터블 컨트롤러, UI를 전부 분리하여 결합도를 낮추려고 노력함
  - 스탯이 포함된 Status 또한 Scriptable Object로 분리
  - EventBus를 활용하여 Status관리
    - Entity 내부의 EventBus를 활용하여 이벤트가 발생 시 처리 행동을 호출
  
      
### 2. 플레이어 이동 로직
  - 점프대 생성
    - 점프대는 일정 시간 딜레이를 줄 수 있도록 설계
    - LayerMask를 넣어주어, 콜라이더에 들어간 Rigidbody 중 LayerMask에 해당하는 오브젝트들만 반응
    - 각 점프대의 힘 및 방향 설정 가능
      
  - 플레이어 이동
    - 점프대에서 받은 힘에 영향을 받으면서 MovePosition으로 이동이 가능하게 설계
    - 점프 시 RayCast를 활용하여 땅에 접촉 해 있는지 확인
      
  - 물건 던지기
    - Grabbable 오브젝트들의 각 설정에 따라 동작함.
    - 박스의 경우 플레이어의 이동 방향과 속도에 따라 박스에 힘을 줘서 날림
   
### 3. 동적생성
  - UI의 경우 동적으로 생성
    - UI프리팹을 만들어서 씬 생성 시점에 인벤토리 및 Crosshair UI 생성

---

## 개발기간

- **2025.08.07(목) ~ 2025.08.13(수)**

---

## 기술스택

### Language
- C#

### Version Control
- Git
- GitHub

### Framework / Engine
- Unity 2022.3 LTS

### IDE
- Unity Editor
- Visual Studio 2022

---

## 📁 프로젝트 파일 구조

-Assets
  - Animation
    -> 점프대 애니메이션 포함
  - Debug
    -> DebugManager 스크립트 및 Prefab
  - Externals
    -> 외부 리소스 포함
  - Material
    -> 스카이 박스 포함
  - PlayerIInput
    -> InputActions 포함
  - Prefabs
    -> 점프대, 플레이어 인벤토리, 상태 프리팹
    - Items
      -> 당근, 박스, 소다, 자판기 프리팹
  - Scenes
    -> MainScene
  - ScriptableObject
    - Item
      -> 당근, 박스, 소다 정보
    - Player
      -> 플래이어 체력 Status
  - Scripts
    -> 게임메니저, PlayerManager
    - Entity
      -> IAttackable, IDamageable, Entity, EntityEventBus
      - Player
        -> 플레이어, 관련 컨트롤러, Grabber, 인벤토리, UI
      - Status
        -> 상태 및 상태 UI
    


## 🛠️ Trouble Shooting

### 플레이어 이동 로직
  - 문제 : 플레이어의 속도를 Velocity로 설정하자, 점프대에 영향을 받지 않게 됨
  - 시도
    - 플레이어의 Position을 직접 바꿔보았으나, 안전하지 않음.
    - 속도 처리를 하는데 있어 AddForce로 전부 처리하기에는 난이도가 너무 높음 및 미끄러지는 현상 발생
  - 결론
    - rigidBody의 MovePosition을 활용하여, 현재 위치에서 이동 할 위치를 더해주는 방식으로 진행
      
### 아이템 상호작용 로직
  - 문제 : 플레이어가 상호작용 하는데 있어, ItemObject를 줍는 행동만을 받으면, 다른 상호작용할 때 다른 처리를 해 줘야함
  - 결론
    - 상호작용을 할 경우, ItemObject또한 IInteractable 인터페이스를 상속받아 통합
    - 주울 수 있는 경우 IInteractable 내부에서 OnInteract에서 처리
    - 상호작용 할 수 있는 경우, IInteractable 내부에서 상호작용 처리
    - Grabbable은 IInteractable과는 다르게 동작함 (키 바인딩 또한 다름)

