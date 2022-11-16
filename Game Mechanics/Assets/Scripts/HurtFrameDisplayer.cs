using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HurtFrameDisplayer : MonoBehaviour
{
    [SerializeField] Color _hurtHue;
    [SerializeField] float _switchInterval;

    Material _mat;


    private void Awake()
    {
        _mat = GetComponent<Renderer>().material;
    }

    public IEnumerator PlayHurtFrames(float time)
    {
        Color original = _mat.color;

        var colorSwitcher = StartCoroutine(SwitchColors(original));

        yield return new WaitForSeconds(time);

        StopCoroutine(colorSwitcher);

        _mat.color = original;
    }

    private IEnumerator SwitchColors(Color original)
    {
        bool onHurtFrame = false;
        while (true)
        {
            if (onHurtFrame)
                _mat.color = original;
            else
                _mat.color = _hurtHue;

            onHurtFrame = !onHurtFrame;

            yield return new WaitForSeconds(_switchInterval);
        }
    }
}
