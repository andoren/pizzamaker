drop schema if exists pizzamaker;
create schema pizzamaker;
create table pizzamaker.dough(
id int  primary key AUTO_INCREMENT,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);
create table pizzamaker.sauce
(
id int  primary key AUTO_INCREMENT,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);
create table pizzamaker.meat
(
id int  primary key AUTO_INCREMENT,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);
create table pizzamaker.topping
(
id int  primary key AUTO_INCREMENT,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);
create table pizzamaker.cheese
(
id int  primary key AUTO_INCREMENT,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);
/*doughs*/
insert into pizzamaker.dough(name,description,price,rawpicture) values("Normal dough","This is a regular dough where we add all the necessary ingridients what is needed to a good pizza dough",0.99,load_file("C:/Pictures/Doughs/normal.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Wholegrain dough","This is made by wholgrain ingredients which is good for your health.",1.35,load_file("C:/Pictures/Doughs/wholegrain.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Multigrain dough","This is made by multigrain ingredients which is good for your health. The nuts are good aminoacid source.",1.50,load_file("C:/Pictures/Doughs/multigrain.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Glutenfree dough","This is made by glutemfree ingredients we strongly recommand for those people who has gluten intolarance",2.0,load_file("C:/Pictures/Doughs/glutenfree.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Crusty dough","This is a regular dough where we add all the necessary ingridients what is needed to a good pizza dough with crusty end.",1.25,load_file("C:/Pictures/Doughs/normal.jpg"));

/*sauces*/
insert into pizzamaker.sauce(name,description,price,rawpicture) values("Tomato sauce","This is a regular souce where we add all the necessary ingridients what is needed to a good pizza sauce",0.55,load_file("C:/Pictures/Sauces/tomato.png"));
insert into pizzamaker.sauce(name,description,price,rawpicture) values("Mustard","It's stone-ground for a traditional tangy flavor. Ingredients: Distilled White Vinegar, Mustard Seed, Water, Salt, Turmeric, Natural Flavor and Spices. ",0.7,load_file("C:/Pictures/Sauces/mustard.png"));
insert into pizzamaker.sauce(name,description,price,rawpicture) values("Spicy sauce","Add some spice to your life with Cholula Hot Sauce Original. This authentic hot sauce is imported straight from Mexico. It adds a fiery taste to your meals whenever you need an extra kick of spice. ",1.00,load_file("C:/Pictures/Sauces/spicy.png"));
insert into pizzamaker.sauce(name,description,price,rawpicture) values("Sour cream","It could be the fresh and delicious taste, the rich and creamy texture, or that it’s made from simple, wholesome ingredients - with no artificial additives or preservatives.",0.45,load_file("C:/Pictures/Sauces/sour.png"));
insert into pizzamaker.sauce(name,description,price,rawpicture) values("Nutella","SpreadMade with skim milk and cocoaOver 100 hazelnuts per jarContains no artificial colors or preservatives Nutella Hazelnut Spread",1.25,load_file("C:/Pictures/Sauces/nutella.png"));
/*meats*/
insert into pizzamaker.meat(name,description,price,rawpicture) values("Chicken breast stripes","Fresh Class A free range corn fed skinless chicken breast fillets.",0.55,load_file("C:/Pictures/Meats/chicken.jpg"));
insert into pizzamaker.meat(name,description,price,rawpicture) values("Fish","Freshly cought fish from the sea. We recommand you to try it.",0.7,load_file("C:/Pictures/Meats/fish.jpg"));
insert into pizzamaker.meat(name,description,price,rawpicture) values("Pork Ham","The unique flavor of our hams comes from the slow smoking methods we have been faithful to for over 141 years. ",1.00,load_file("C:/Pictures/Meats/ham.jpg"));
insert into pizzamaker.meat(name,description,price,rawpicture) values("Pulled pork","Pulled Pork is an American culinary dish that originated in the southern US states which uses shredded barbecued pork shoulder as the main ingredient.",1.25,load_file("C:/Pictures/Meats/pulled.png"));
insert into pizzamaker.meat(name,description,price,rawpicture) values("Salami slices","Sliced seasoned pork salami. This perfectly seasoned Italian salami is traditionally dry cured by a Italian family run company with 8 generations of expertise.",0.45,load_file("C:/Pictures/Meats/salami.jpg"));
insert into pizzamaker.meat(name,description,price,rawpicture) values("Sausage","Pork sausage meat filled into natural pork casings.",1.25,load_file("C:/Pictures/Meats/sausage.jpg"));
/*Topping*/
insert into pizzamaker.topping(name,description,price,rawpicture) values("Tomato","Freshly sliced tomato is always a perfect choice",0.1,load_file("C:/Pictures/Toppings/tomato.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Mushroom ","Freshly sliced mushroom is always a perfect choice",0.2,load_file("C:/Pictures/Toppings/mushroom.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Pepper","Freshly sliced pepper is always a perfect choice",0.3,load_file("C:/Pictures/Toppings/pepper.png"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Pineapple","The most valami ingredient in the world",1.1,load_file("C:/Pictures/Toppings/pineapple.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Banana","This ingridients is for the nutella souce",1.2,load_file("C:/Pictures/Toppings/banana.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Olive","Fresh olive straight from the shop.",0.4,load_file("C:/Pictures/Toppings/olive.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Rocket","Fresh rocket",0.1,load_file("C:/Pictures/Toppings/rocket.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Corn","Caned corn",0.2,load_file("C:/Pictures/Toppings/corn.jpg"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Bean","Caned bean",0.3,load_file("C:/Pictures/Toppings/bean.png"));
insert into pizzamaker.topping(name,description,price,rawpicture) values("Eggs","Boiled egg slices",0.4,load_file("C:/Pictures/Toppings/egg.jpg"));
