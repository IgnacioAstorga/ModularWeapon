using System;
using System.Collections.Generic;
using System.Collections;

[Serializable]
public class Sequence<T> : IEnumerator<T> {

	object IEnumerator.Current { get { return Current; } }
	public T Current { get { return elements[_currentIndex]; } }

	public CycleType cycleType;
	public T[] elements;

	private int _currentIndex;
	private bool _backwards;

	public Sequence() {
		Reset();
	}

	public void Dispose() {
		// Do nothing
	}

	public bool MoveNext() {
		if (_currentIndex == -1) {
			_currentIndex = 0;
			return false;
		}

		switch (cycleType) {
			case CycleType.Fixed:
				return true;
			case CycleType.Sequential:
				return JumpNext();
			case CycleType.Inverse:
				return JumpPrevious();
			case CycleType.BackAndForward:
				if (!_backwards) {
					if (JumpNext()) {
						_backwards = true;
						_currentIndex = elements.Length - 1;
					}
					return false;
				}
				else {
					if (JumpPrevious()) {
						_backwards = false;
						_currentIndex = 0;
						return true;
					}
					return false;
				}
			case CycleType.Random:
				_currentIndex = UnityEngine.Random.Range(0, elements.Length);
				return true;
			default:
				return true;
		}
	}

	public bool MovePrevious() {
		if (_currentIndex == -1) {
			_currentIndex = 0;
			return false;
		}

		switch (cycleType) {
			case CycleType.Fixed:
				return true;
			case CycleType.Sequential:
				return JumpPrevious();
			case CycleType.Inverse:
				return JumpNext();
			case CycleType.BackAndForward:
				if (!_backwards) {
					if (JumpPrevious()) {
						_backwards = true;
						_currentIndex = 0;
						return true;
					}
					return false;
				}
				else {
					if (JumpNext()) {
						_backwards = false;
						_currentIndex = elements.Length - 1;
					}
					return false;
				}
			case CycleType.Random:
				_currentIndex = UnityEngine.Random.Range(0, elements.Length - 1);
				return true;
			default:
				return true;
		}
	}

	public void Set(int index) {
		_currentIndex = index;
	}

	public void Reset() {
		_currentIndex = -1;
		_backwards = false;
	}

	public bool JumpNext() {
		_currentIndex++;
		if (_currentIndex >= elements.Length) {
			_currentIndex = 0;
			return false;
		}
		else
			return true;
	}

	public bool JumpPrevious() {
		_currentIndex--;
		if (_currentIndex < 0) {
			_currentIndex = elements.Length - 1;
			return false;
		}
		else
			return true;
	}
}

public enum CycleType {
	Fixed,
	Sequential,
	Inverse,
	BackAndForward,
	Random
}