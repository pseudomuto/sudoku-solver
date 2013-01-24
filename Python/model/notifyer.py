class Notifyer(object):
	def __init__(self):
		self.listeners = {}

	def fireEvent(self, eventName, data = None):
		if eventName in self.listeners:
			for responder in self.listeners[eventName]:
				responder(data)

	def addListener(self, eventName, responder):
		if not eventName in self.listeners:
			self.listeners[eventName] = []

		self.listeners[eventName].append(responder)

	def removeListener(self, eventName, responder):
		if eventName in self.listeners:
			if responder in self.listeners[eventName]:
				self.listeners[eventName].remove(responder)