using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using static Village.Scriptables.Effect;
using static Village.Scriptables.Resource;

namespace Village.Scriptables
{
	[CreateAssetMenu(fileName = "Event", menuName = "Village/Event")]
	public partial class EventBase : ScriptableObject
	{
		public string title;

		[TextArea(4, 4)]
		public string description;

		public int turnDuration;

		[FormerlySerializedAs("requirements")]
		public List<ResourceAmount> requirements;

		[SerializeField]
		public List<EffectAmount> onSuccess;

		[SerializeField]
		public List<EffectAmount> onFailure;

		public void ApplySuccess()
		{
			foreach (var eff in onSuccess)
			{
				eff.effect.Apply(eff.value);
			}
		}

		public void ApplyFailure()
		{
			foreach (var eff in onFailure)
			{
				eff.effect.Apply(eff.value);
			}
		}
	}
}