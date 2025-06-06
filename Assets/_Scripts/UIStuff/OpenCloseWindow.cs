using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OpenCloseWindow : MonoBehaviour
{
    [Header("Window Setup")]
    [SerializeField] private GameObject window;

    [SerializeField] private RectTransform windowRectTransform;
    [SerializeField] private CanvasGroup windowCanvasGroup;

    public enum AnimateToDirection
    {
        Top,
        Bottom,
        Left,
        Right
    }

    [Header("Animation Setup")]
    [SerializeField] private AnimateToDirection openDirection = AnimateToDirection.Top;
    [SerializeField] private AnimateToDirection closeDirection = AnimateToDirection.Bottom;
    [Space]
    [SerializeField] private Vector2 distanceToAnimate = new Vector2(100, 100);
    [SerializeField] private AnimationCurve easingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [Range(0, 1f)][SerializeField] private float animationDuration = 0.5f;

    private bool _isOpen;
    private Vector2 _initialPosition;
    private Vector2 _currentPosition;

    private Vector2 _upOffset;
    private Vector2 _downOffset;
    private Vector2 _leftOffset;
    private Vector2 _rightOffset;

    private Coroutine _animateWindowCoroutine;

    [Header("Helpers")]
    [SerializeField] private bool displayGizmos = true;

    public static event Action OnOpenWindow;
    public static event Action OnCloseWindow;

    private void OnValidate()
    {
        if (window != null)
        {
            windowRectTransform = window.GetComponent<RectTransform>();
            windowCanvasGroup = window.GetComponent<CanvasGroup>();
        }

        distanceToAnimate.x = Mathf.Max(0, distanceToAnimate.x);
        distanceToAnimate.y = Mathf.Max(0, distanceToAnimate.y);


        _initialPosition = window.transform.position;

    }

    #region AnimationFunctionality

    private void Start()
    {
        _initialPosition = window.transform.position;

        InitializeOffsetPositions();
    }

    private void InitializeOffsetPositions()
    {
        _upOffset = new Vector2(0, distanceToAnimate.y);
        _downOffset = new Vector2(0, -distanceToAnimate.y);

        _rightOffset = new Vector2(+distanceToAnimate.x, 0);
        _leftOffset = new Vector2(-distanceToAnimate.x, 0);
    }

    [ContextMenu("Toggle Open Close")]
    public void ToggleOpenClose()
    {
        if (_isOpen)
            CloseWindow();
        else
            OpenWindow();
    }

    [ContextMenu("Open Window")]
    public void OpenWindow()
    {
        if (_isOpen)
            return;

        _isOpen = true;
        OnOpenWindow?.Invoke();

        if (_animateWindowCoroutine != null)
            StopCoroutine(_animateWindowCoroutine);

        _animateWindowCoroutine = StartCoroutine(AnimateWindow(true));
    }

    [ContextMenu("Close Window")]
    public void CloseWindow()
    {
        if (!_isOpen)
            return;

        _isOpen = false;
        OnCloseWindow?.Invoke();

        if (_animateWindowCoroutine != null)
            StopCoroutine(_animateWindowCoroutine);

        _animateWindowCoroutine = StartCoroutine(AnimateWindow(false));
    }

    private Vector2 GetOffset(AnimateToDirection direction)
    {
        switch (direction)
        {
            case AnimateToDirection.Top:
                return _upOffset;
            case AnimateToDirection.Bottom:
                return _downOffset;
            case AnimateToDirection.Left:
                return _leftOffset;
            case AnimateToDirection.Right:
                return _rightOffset;
            default:
                return Vector3.zero;
        }
    }

    private IEnumerator AnimateWindow(bool open)
    {
        if (open)
            window.gameObject.SetActive(true);

        _currentPosition = window.transform.position;

        float elapsedTime = 0;

        Vector2 targetPosition = _currentPosition;

        if (open)
            targetPosition = _currentPosition + GetOffset(openDirection);
        else
            targetPosition = _currentPosition + GetOffset(closeDirection);

        while (elapsedTime < animationDuration)
        {
            float evaluationAtTime = easingCurve.Evaluate(elapsedTime / animationDuration);

            window.transform.position = Vector2.Lerp(_currentPosition, targetPosition, evaluationAtTime);

            windowCanvasGroup.alpha = open
                ? Mathf.Lerp(0f, 1f, evaluationAtTime)
                : Mathf.Lerp(1f, 0f, evaluationAtTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        window.transform.position = targetPosition;

        windowCanvasGroup.alpha = open ? 1 : 0;
        windowCanvasGroup.interactable = open;
        windowCanvasGroup.blocksRaycasts = open;

        if (!open)
        {
            window.gameObject.SetActive(false);
            window.transform.position = _initialPosition;
        }
    }

    #endregion
}