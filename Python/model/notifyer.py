class Notifyer(object):
	"""A simple class for handling event notifications"""

	def __init__(self):
		self.listeners = {}

	def fireEvent(self, eventName, data = None):
		"""Notifies all registered listeners that the specified event has occurred

		eventName: The name of the event being fired
		data: An optional parameter to be passed on listeners
		"""
		if eventName in self.listeners:
			for responder in self.listeners[eventName]:
				responder(data)

	def addListener(self, eventName, responder):
		"""Registers responder as a listener for the specified event

		eventName: The name of the event to listen for
		responder: A callback method that will be notified when the event occurs
		"""
		if not eventName in self.listeners:
			self.listeners[eventName] = []

		self.listeners[eventName].append(responder)

	def removeListener(self, eventName, responder):
		"""Removes the specified listener from the set of observers

		eventName: The name of the event to stop listening for
		responder: The callback method to remove
		"""
		if eventName in self.listeners:
			if responder in self.listeners[eventName]:
				self.listeners[eventName].remove(responder)