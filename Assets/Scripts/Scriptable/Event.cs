using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName ="Event", menuName ="Village/Event")]
public class Event : ScriptableObject
{
	public string title;

	[TextArea]
	public string description;

	public int turnDuration;

	public List<ResourceAmount> requirements;

	public List<EffectAmount> onSuccess;

	public List<EffectAmount> onFailure;
}

