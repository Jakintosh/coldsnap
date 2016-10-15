using System;
using System.Collections.Generic;

public static class NotificationCenter {

	private static Dictionary<Notification, List<Action>> _registration = new Dictionary<Notification, List<Action>>();

	public static void RegisterForNotification ( Notification notification, Action action ) {

		if ( !_registration.ContainsKey( notification ) ) {
			_registration[notification] = new List<Action>();
		}
		_registration[notification].Add(action);
	}

	public static void DeregisterForNotification ( Notification notification, Action action ) {

		if ( _registration.ContainsKey( notification ) ) {
			var registrant = _registration[notification];
			if ( registrant.Contains( action ) ) {
				registrant.Remove( action );
			}
		}
	}

	public static void PostNotification ( Notification notification ) {

		if ( _registration.ContainsKey( notification ) ) {
			var actions = _registration[notification];
			foreach ( Action a in actions ) {
				a ();
			}
		}
	}

}

public enum Notification {
	START_MENU_DISMISSED,
	MATCH_SETTINGS_CONFIRMED
}