using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private bool _grabbing;

    void Start()
    {
        _grabbing = false;
    }

    void Update()
    {
    }

    public void grabbing_set(bool isgrabbing)
    {
        _grabbing = isgrabbing;
    }

    public bool grabbing_get()
    {
        return _grabbing;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            var sound = other.GetComponent<Squishy_grab>();

            if (_grabbing)
            {
                sound.GrabItem();
                PlaySound(other);
            }
            else
            {
                sound.DropItem();
                // Optionally play drop sound here if needed
            }
        }

        else if (other.CompareTag("Flask"))
        {
            var sound = other.GetComponent<Glass_grab>();

            if (_grabbing)
            {
                sound.GrabItem();
                PlaySound(other);
            }
            else
            {
                sound.DropItem();
                // Optionally play drop sound here if needed
            }
        }
    }

    private void PlaySound(Collider other)
    {
        if (other.name.Contains("Flask"))
        {
            AudioManager.Instance.PlaySound("Glass");
            Debug.Log("Glass sounds should be played");
        }
        else if (ContainsAny(other.name, new string[] { "Beetle", "Eye", "Frog", "Tail" }))
        {
            AudioManager.Instance.PlaySound("squishy");
        }
        else if (other.name.Contains("Pouch"))
        {
            AudioManager.Instance.PlaySound("pouch");
        }
    }

    private bool ContainsAny(string source, string[] substrings)
    {
        foreach (string substring in substrings)
        {
            if (source.Contains(substring))
            {
                return true;
            }
        }
        return false;
    }
}