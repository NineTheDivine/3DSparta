# 3DSparta
Unity Sparta Bootcamp Chapter 5
Version 1.1.0


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
   
  - 움직이는 플래폼 생성
    - 특정 위치까지 이동하는 플랫폼 생성
    - 속도와 시간, 목표 지점 설정 가능
    - Physics Material을 활용하여 플레이어의 미끄러짐 방지
   
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
    -> 점프대, 플레이어 인벤토리, 상태, 이동식 발판 프리팹
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
    - Interactables
      -> IInteractable 인터페이스
      - Grabbable Object
        -> IGrabable, IGrab, BasicBox
      - Obtainable
        -> Iteminfo, ItemObject, IUseable
        - Useables
          -> 사용 가능한 Usable 함수들
        - VendingMachine
          -> 아이템 소환을 위한 VendingMachine
    - MapObjects
      -> PlatformTrigger
      - JumpPad
        -> 점프패드 관련 스크립트
      - MovingPlatform
        -> 이동식 플랫폼 관련 스크립트
    


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

### 움직이는 플랫폼 이동 로직
  - 문제 1 : 움직이는 플랫폼 위에 오브젝트가 올라갔을 경우, 원하는 방향대로 이동하지 않는 경우 발생
  - 시도
    - 위에 있는 rigidbody에 직접 MovePosition을 추가 해 주려고 하였으나, 플레이어의 경우 이동을 MovePosition으로 처리하기 때문에, 이동이 되지 않는 현상 발생
  - 결론
    - 우선 해당 콜라이더에 부딪쳤을 경우, 해당 플랫퐁으로 부모를 설정하고, 돌아갈 시 해제하는 방식 사용

  - 문제 2: 플레이어가 플랫폼 위에 올라갈 경우, 플레이어의 이동이 제어되는 현상이 발생됨
  - 결론
    - MovingPlatform에 이동을 Collider로만 처리하고 있었는데, 이에 RigidBody를 달아주기로 결심함
    - 단, 이제 플랫폼의 이동을 고정화 하기 위하여, isKinematic을 설정해주고, 이동하지 않는 방향의 경우 Platform의 Position을 스크립트 내에서 고정시킴

  - 문제 3: 플레이어가 콜라이더를 반복해서 나갔다 들어왔다 하는 현상 발생
  - 원인 : 콜라이더가 이동을 하며, rigidbody가 플레이어에게 영향을 주며 이동시킴
  - 결론
    - Physics.Raycast를 활용하여 플레이어가 플랫폼 위에있다면, 플럇폼에 나갔다고 처리하지 않음
    - 반대로 플레이어가 플랫폼 위에 있지 않다면, 플랫폼에 들어오지 않았다고 처리함

  - 문제 4 : 물체들과 플레이어가 플랫폼 위에서 미끄러져 떨어지는 현상 발생
  - 원인 : 플랫폼의 Rigidbody에서 주어진 속도가 위에 올라가있는 오브젝트에 영향을 주기 때문에, 이동을 할 시 오브젝트에 속도가 붙어서 정지시 유지된 속도로 인해 떨어짐
  - 시도
    - 위에 있는 물체들에 MovePosition을 활용할까 해 보았지만, 플레이어의 MovePosition 이동로직 때문에 처리가 안됨
    - Addforce로 모두 처리하기에는 부정확하고 복잡해짐
  - 결론
    - 결국 마찰력이 적어 플랫폼에서 미끄러지는것으로 파악
    - 플랫폼을 위한 Physics Material을 제작하여, BoxCollider 내부에 높은 마찰력을 부여하여 붙여줌
    - 플레이어 및 오브젝트는 마찰력의 영향을 받아, 더이상 미끄러지지 않게 됨.
  
