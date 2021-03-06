﻿using System;
using NodeEditor.Controls;
using UnityEngine;

namespace NodeEditor.Nodes
{
	public class GlobalPropertyNode<T> : AbstractNode
	{
		private Guid m_PropertyReference;
		[SerializeField]
		private string m_PropertyReferenceName;

		[DefaultControl]
		public string propertyReferenceName
		{
			get { return m_PropertyReferenceName; }
			set
			{
				m_PropertyReferenceName = value;
				m_PropertyReference = StringToGUID.Get(m_PropertyReferenceName);
			}
		}

		public GlobalPropertyNode()
		{
			name = "Global Property";
			CreateOutputSlot<GetterSlot<T>>("Out").SetGetter(GetValue);
		}

		private T GetValue()
		{
			T val;
			GlobalProperties<T>.TryGetValue(m_PropertyReference, out val);
			return val;
		}

		public override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();
			m_PropertyReference = StringToGUID.Get(m_PropertyReferenceName);
		}
	}
}