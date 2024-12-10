import numpy as np
values = [-0.14016034,-1.08858967,0.01164871,0.57597578,1.10593287]


num = 0 
for i in values : 
    num+=np.sum((i)**2)

print(num)