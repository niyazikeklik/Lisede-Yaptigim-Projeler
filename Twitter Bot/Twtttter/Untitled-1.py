while True:
 x=pyautogui.position()
 print(x+pyautogui.pixel(x[0],x[1]))