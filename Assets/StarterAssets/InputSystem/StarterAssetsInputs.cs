using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public int  currentWeapon;
		public bool changeWeapon;
		public bool fire;
		public bool fire2;
		public bool map;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("UI Values")]
		public bool PauseButtonDown = false;
		

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
			
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnWeapon1(InputValue value)
		{
			WeaponChanger(value.isPressed, 0);				
		}

		public void OnWeapon2(InputValue value)
		{
			WeaponChanger(value.isPressed, 1);
		}

		public void OnWeapon3(InputValue value)
		{
			WeaponChanger(value.isPressed, 2);
		}

		public void OnUseWeapon(InputValue value)
		{
			FireInput(value.isPressed);

		}

		public void OnFire2(InputValue value)
		{
			FireInput2(value.isPressed);

		}

		public void OnPause(InputValue value) 
		{
			PauseInput (value.isPressed);
		}

		public void OnMap(InputValue value)
		{
			PauseInput(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif
		public void MapInput(bool newMapState)
		{
			map = newMapState;
		}

		public void PauseInput(bool newPauseState)
        {
			PauseButtonDown = newPauseState;
        }


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void WeaponChanger(bool newWeaponState, int selectedWeapon)
        {
			if (newWeaponState)
				currentWeapon = selectedWeapon;
        }

		public void FireInput(bool newFireState)
        {
			fire = newFireState;
        }

		public void FireInput2(bool newFire2State)
		{
			fire2 = newFire2State;
		}


		private void OnApplicationFocus(bool hasFocus)
		{
			if(!GameManager.Instance.IsGameOver || !GameManager.Instance.Ispaused)
            {
				SetCursorState(cursorLocked);
			}          
		
		}
		private void SetCursorState(bool newState)
		{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

	}
	
}