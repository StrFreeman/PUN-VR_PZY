/*
 * Copyright (C) 2012-2023 MotionSystems
 *
 * This file is part of ForceSeatMI SDK.
 *
 * www.motionsystems.eu
 *
 */

using UnityEngine;

namespace MotionSystems
{
	class ForceSeatMI_Vehicle : ForceSeatMI_ITelemetryInterface
	{
		const float FSMI_VT_ACC_LOW_PASS_FACTOR          = 0.5f;
		const float FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR = 0.5f;
		const float FSMI_LINEAR_SPEED_LOW_PASS_FACTOR    = 0.5f;
		const float FSMI_BODY_PRY_LOW_PASS_FACTOR        = 0.5f;

		private bool m_firstCall         = true;
		private float m_prevForwardSpeed = 0;
		private float m_prevRightSpeed   = 0;
		private float m_prevUpSpeed      = 0;
		private Vector3 m_prevAngles     = new Vector3();
		private Rigidbody m_rb           = null;
		private sbyte m_gearNumber       = 0;
		private uint m_rpm               = 0;
		private uint m_maxRpm            = 0;

		public ForceSeatMI_Vehicle(Rigidbody rb)
		{
			m_rb = rb;
		}

		public virtual void Begin()
		{
			m_firstCall = true;
		}

		public virtual void End()
		{
			m_firstCall = true;
		}

		public virtual void FixedUpdate(float fixedDeltaTime, ref FSMI_TelemetryACE telemetry)
		{
			var velocity     = m_rb.transform.InverseTransformDirection(m_rb.velocity);
			var forwardSpeed = velocity.z;
			var rightSpeed   = velocity.x;
			var upSpeed      = velocity.y;

			telemetry.rpm                    = m_rpm;
			telemetry.maxRpm                 = m_maxRpm;
			telemetry.vehicleForwardSpeed    = velocity.magnitude; // m/s

			if (m_firstCall)
			{
				m_firstCall = false;

				telemetry.bodyLinearAcceleration[0].forward = 0;
				telemetry.bodyLinearAcceleration[0].right   = 0;
				telemetry.bodyLinearAcceleration[0].upward  = 0;
				telemetry.bodyAngularVelocity[0].roll       = 0;
				telemetry.bodyAngularVelocity[0].pitch      = 0;
				telemetry.bodyAngularVelocity[0].yaw        = 0;
				telemetry.bodyLinearVelocity[0].right       = 0;
				telemetry.bodyLinearVelocity[0].upward      = 0;
				telemetry.bodyLinearVelocity[0].forward     = 0;
				telemetry.bodyPitch                         = 0;
				telemetry.bodyRoll                          = 0;
			}
			else
			{
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyLinearAcceleration[0].forward, (forwardSpeed - m_prevForwardSpeed) / fixedDeltaTime, FSMI_VT_ACC_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyLinearAcceleration[0].right,   (rightSpeed   - m_prevRightSpeed)   / fixedDeltaTime, FSMI_VT_ACC_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyLinearAcceleration[0].upward,  (upSpeed      - m_prevUpSpeed)      / fixedDeltaTime, FSMI_VT_ACC_LOW_PASS_FACTOR);

				var deltaAngles = new Vector3(
					Mathf.Deg2Rad * Mathf.DeltaAngle(m_rb.transform.eulerAngles.x, m_prevAngles.x),
					Mathf.Deg2Rad * Mathf.DeltaAngle(m_rb.transform.eulerAngles.y, m_prevAngles.y),
					Mathf.Deg2Rad * Mathf.DeltaAngle(m_rb.transform.eulerAngles.z, m_prevAngles.z)
				);

				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyAngularVelocity[0].roll,  deltaAngles.z / fixedDeltaTime, FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyAngularVelocity[0].pitch, deltaAngles.x / fixedDeltaTime, FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyAngularVelocity[0].yaw,   deltaAngles.y / fixedDeltaTime, FSMI_VT_ANGLES_SPEED_LOW_PASS_FACTOR);

				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyLinearVelocity[0].right,   rightSpeed,   FSMI_LINEAR_SPEED_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyLinearVelocity[0].upward,  upSpeed,      FSMI_LINEAR_SPEED_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyLinearVelocity[0].forward, forwardSpeed, FSMI_LINEAR_SPEED_LOW_PASS_FACTOR);

				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyPitch, -ForceSeatMI_Utils.MotionFriendlyPRY_rad(Mathf.Deg2Rad * m_rb.transform.eulerAngles.x), FSMI_BODY_PRY_LOW_PASS_FACTOR);
				ForceSeatMI_Utils.LowPassFilter(ref telemetry.bodyRoll,  -ForceSeatMI_Utils.MotionFriendlyPRY_rad(Mathf.Deg2Rad * m_rb.transform.eulerAngles.z), FSMI_BODY_PRY_LOW_PASS_FACTOR);

			}

			m_prevForwardSpeed = forwardSpeed;
			m_prevRightSpeed   = rightSpeed;
			m_prevUpSpeed      = upSpeed;
			m_prevAngles.x     = m_rb.transform.eulerAngles.x;
			m_prevAngles.y     = m_rb.transform.eulerAngles.y;
			m_prevAngles.z     = m_rb.transform.eulerAngles.z;

			telemetry.gearNumber = m_gearNumber;
		}

		public virtual void Pause(bool paused)
		{
			m_firstCall = true;
		}

		public void SetGearNumber(int gearNumber)
		{
			m_gearNumber = (sbyte)gearNumber;
		}

		public void SetRpm(uint rpm)
		{
			m_rpm = rpm;
		}

		public void SetMaxRpm(uint maxRpm)
		{
			m_maxRpm = maxRpm;
		}
	}
}
