import numpy as np

# Define the objective function (example: Sphere function)
def objective_function(x):
    return np.sum(x**2)

# Shuffled Frog Leaping Algorithm
def shuffled_frog_leaping_algorithm(num_frogs, num_variables, max_iter, lower_bound, upper_bound):
    # Initialize the frog population randomly within the bounds
    frogs = np.random.uniform(lower_bound, upper_bound, (num_frogs, num_variables))
    fitness = np.apply_along_axis(objective_function, 1, frogs)
    
    # Main loop of the algorithm
    for iteration in range(max_iter):
        # Sort frogs based on their fitness values
        sorted_indices = np.argsort(fitness)
        frogs = frogs[sorted_indices]
        fitness = fitness[sorted_indices]
        
        # Divide the population into groups
        num_groups = num_frogs // 2
        group1 = frogs[:num_groups]
        group2 = frogs[num_groups:]
        
        # Perform the "leap" operation for group1
        for i in range(num_groups):
            for j in range(num_variables):
                # Randomly update the frog's position in group1
                leap_size = np.random.uniform(-1, 1) * (frogs[i, j] - np.mean(group2[:, j]))
                frogs[i, j] += leap_size
                frogs[i, j] = np.clip(frogs[i, j], lower_bound, upper_bound)
        
        # Evaluate the fitness of the frogs after the leap
        fitness = np.apply_along_axis(objective_function, 1, frogs)
        
        # Shuffling step: shuffle the frogs' positions between groups
        np.random.shuffle(frogs)
        
        # Print the best fitness found in each iteration
        print(f"Iteration {iteration + 1}, Best Fitness: {fitness[0]}")
    
    # Return the best solution found
    best_frog = frogs[0]
    best_fitness = fitness[0]
    return best_frog, best_fitness

# Parameters
num_frogs = 30            # Number of frogs
num_variables = 5         # Number of variables (dimensions)
max_iter = 100            # Maximum number of iterations
lower_bound = -5          # Lower bound for the variables
upper_bound = 5           # Upper bound for the variables

# Run the algorithm
best_solution, best_fitness = shuffled_frog_leaping_algorithm(num_frogs, num_variables, max_iter, lower_bound, upper_bound)

print("Best Solution:", best_solution)
print("Best Fitness:", best_fitness)
