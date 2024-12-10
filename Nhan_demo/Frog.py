import tkinter as tk




class window : 

	# Create a window
	def __init__(self): 
		self.tk = 
	window = tk.Tk()
	window.title("2D Object Display")
	window.geometry("400x400")

	# Create a Canvas to draw shapes
	canvas = tk.Canvas(window, width=400, height=400, bg="white")
	canvas.pack()

	# Draw objects on the Canvas
	canvas.create_rectangle(50, 50, 150, 150, fill="blue", outline="black")  # Rectangle
	canvas.create_oval(200, 200, 300, 300, fill="red", outline="black")      # Circle

	# Run the window loop
	window.mainloop()
