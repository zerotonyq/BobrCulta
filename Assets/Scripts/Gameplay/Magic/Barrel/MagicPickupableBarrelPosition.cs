using Gameplay.Core.Pickup.Base;
using UnityEngine;

namespace Gameplay.Magic.Barrel
{
    public class MagicPickupableBarrelPosition : MonoBehaviour
    {
        [field: SerializeField] public Pickupable Pickupable { get; private set; }

        public void Set(Pickupable pickupable)
        {
            if (Pickupable)
            {
                Pickupable.GetComponent<Rigidbody>().isKinematic = false;
                Pickupable.GetComponent<Collider>().enabled = true;
                Pickupable.transform.SetParent(null);
                Pickupable.gameObject.layer = 1 >> LayerMask.NameToLayer("Default");
                Pickupable.transform.localScale *= 10;
                Pickupable.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
            }

            if (pickupable)
            {
                pickupable.GetComponent<Rigidbody>().isKinematic = true;

                pickupable.Reset();

                pickupable.GetComponent<Collider>().enabled = false;

                pickupable.transform.SetParent(transform);

                pickupable.transform.localRotation = Quaternion.identity;

                pickupable.transform.localPosition = Vector3.zero;

                pickupable.transform.localScale /= 10;

                pickupable.gameObject.layer = gameObject.layer;
            }

            Pickupable = pickupable;
        }
    }
}