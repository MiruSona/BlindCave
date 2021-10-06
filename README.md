# BlindCave_GGJ

## 프로젝트 설명
* GGJ2017(Global Game Jam)에서 'Waves'라는 주제로 만든 게임
* 무박 3일간 개발함
* 소리만으로 주변을 밝힐 수 있는 동굴에서 아내를 살리기 위한 꽃을 구하러 간다는 컨셉
* 주인공이 땅에 닿을때, 또는 소리를 지속적으로 내는 꽃으로만 주변 시야를 밝힐 수 있음
* 제한된 시야와 3종류의 꽃을 이용한 퍼즐이 특징
* 게임잼 게시글 : [링크](https://globalgamejam.org/2017/games/blind-cave)

## 영상(이미지 클릭 시 유투브로 이동)
[![BlindCave](https://img.youtube.com/vi/SzVPHOrWsBQ/0.jpg)](https://youtu.be/SzVPHOrWsBQ "BlindCave")

## 개발환경
* Unity 5.5.0f3
* 안드로이드 및 PC
* 스프라이트 에셋 사용 : https://www.kenney.nl/assets

## 프로젝트 팀원 및 역할
* 사운드(BGM) 1인, 프로그래머 1인 개발
* 나의 역할 : 프로그래머

## 개발 기능
* 플레이어 및 UI
	* 모바일 : 화면의 중앙부분을 기준으로 왼쪽을 터치하면 왼쪽으로,
	오른쪽을 터치하면 오른쪽으로 이동하도록 구현
	* PC : 좌우 방향키로 이동
	* 공통 : 우측 상단에 꽃 스프라이트로 목숨 표시
* 기믹
	* 꽃 : 닿으면 획득 가능하며, 플레이어 머리위에 꽃 스프라이트가 나타남
	꽃잎이 일정시간마다 줄어들면서 소리와 함께 주변 시야를 밝혀줌
		* 노란꽃 : 목표지점. 닿으면 넓은범위의 시야를 밝혀주며, 다음 스테이지로 넘어감
		* 파란꽃 : 소리(원형이펙트)에 닿은 보이지 않는 블럭을 보여줌.
		* 보라꽃 : 닿으면 엔딩.
	* 가시 : 닿으면 피격음과 함께 주변 시야를 밝혀주며, 플레이어의 목숨이 하나 줄어듬.
	* 사라지는 블럭 : 플레이어가 닿는 순간부터 투명해짐. 완전 투명해지면 컬리션을 비활성화해
	밟을 수가 없게됨. 일정 시간 뒤 다시 나타나며, 컬리션도 활성화
	* 움직이는 블럭 : 지정해둔 이동경로대로 계속 움직임
* 시야 연출
	* 쉐이더 및 C# 스크립트 사용
	* C# 스크립트에서 카메라 화면을 받아와 쉐이더에 텍스쳐로 넘기는 방식으로 구현
	* 원형으로 퍼지는 이펙트는 파티클 시스템 사용
	* 코드 : [쉐이더](https://github.com/MiruSona/BlindCave/blob/main/Assets/Script/Shader/DarkShader.shader), [C#코드](https://github.com/MiruSona/BlindCave/blob/main/Assets/Script/Util/DarkShader.cs)