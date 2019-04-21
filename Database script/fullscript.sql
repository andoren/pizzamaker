drop schema if exists pizzamaker;
create schema pizzamaker;
create table pizzamaker.foods(
id int  primary key AUTO_INCREMENT,
type varchar(10) not null,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);

/*doughs*/
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Normal dough","dough","This is a regular dough where we add all the necessary ingridients what is needed to a good pizza dough",0.99,load_file("C:/Pictures/Doughs/normal.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Wholegrain dough","dough","This is made by wholgrain ingredients which is good for your health.",1.35,load_file("C:/Pictures/Doughs/wholegrain.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Multigrain dough","dough","This is made by multigrain ingredients which is good for your health. The nuts are good aminoacid source.",1.50,load_file("C:/Pictures/Doughs/multigrain.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Glutenfree dough","dough","This is made by glutemfree ingredients we strongly recommand for those people who has gluten intolarance",2.0,load_file("C:/Pictures/Doughs/glutenfree.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Crusty dough","dough","This is a regular dough where we add all the necessary ingridients what is needed to a good pizza dough with crusty end.",1.25,load_file("C:/Pictures/Doughs/normal.jpg"));

/*sauces*/
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Tomato sauce","sauce","This is a regular souce where we add all the necessary ingridients what is needed to a good pizza sauce",0.55,load_file("C:/Pictures/Sauces/tomato.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Mustard","sauce","It's stone-ground for a traditional tangy flavor. Ingredients: Distilled White Vinegar, Mustard Seed, Water, Salt, Turmeric, Natural Flavor and Spices. ",0.7,load_file("C:/Pictures/Sauces/mustard.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Spicy sauce","sauce","Add some spice to your life with Cholula Hot Sauce Original. This authentic hot sauce is imported straight from Mexico. It adds a fiery taste to your meals whenever you need an extra kick of spice. ",1.00,load_file("C:/Pictures/Sauces/spicy.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Sour cream","sauce","It could be the fresh and delicious taste, the rich and creamy texture, or that it’s made from simple, wholesome ingredients - with no artificial additives or preservatives.",0.45,load_file("C:/Pictures/Sauces/sour.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Nutella","sauce","SpreadMade with skim milk and cocoaOver 100 hazelnuts per jarContains no artificial colors or preservatives Nutella Hazelnut Spread",1.25,load_file("C:/Pictures/Sauces/nutella.png"));
/*meats*/
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Chicken breast stripes","meat","Fresh Class A free range corn fed skinless chicken breast fillets.",0.55,load_file("C:/Pictures/Meats/chicken.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Fish","meat","Freshly cought fish from the sea. We recommand you to try it.",0.7,load_file("C:/Pictures/Meats/fish.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Pork Ham","meat","The unique flavor of our hams comes from the slow smoking methods we have been faithful to for over 141 years. ",1.00,load_file("C:/Pictures/Meats/ham.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Pulled pork","meat","Pulled Pork is an American culinary dish that originated in the southern US states which uses shredded barbecued pork shoulder as the main ingredient.",1.25,load_file("C:/Pictures/Meats/pulled.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Salami slices","meat","Sliced seasoned pork salami. This perfectly seasoned Italian salami is traditionally dry cured by a Italian family run company with 8 generations of expertise.",0.45,load_file("C:/Pictures/Meats/salami.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Sausage","meat","Pork sausage meat filled into natural pork casings.",1.25,load_file("C:/Pictures/Meats/sausage.jpg"));
/*Topping*/
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Tomato","topping","Freshly sliced tomato is always a perfect choice",0.1,load_file("C:/Pictures/Toppings/tomato.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Mushroom ","topping","Freshly sliced mushroom is always a perfect choice",0.2,load_file("C:/Pictures/Toppings/mushroom.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Pepper","topping","Freshly sliced pepper is always a perfect choice",0.3,load_file("C:/Pictures/Toppings/pepper.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Pineapple","topping","The most valami ingredient in the world",1.1,load_file("C:/Pictures/Toppings/pineapple.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Banana","topping","This ingridients is for the nutella souce",1.2,load_file("C:/Pictures/Toppings/banana.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Olive","topping","Fresh olive straight from the shop.",0.4,load_file("C:/Pictures/Toppings/olive.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Rocket","topping","Fresh rocket",0.1,load_file("C:/Pictures/Toppings/rocket.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Corn","topping","Caned corn",0.2,load_file("C:/Pictures/Toppings/corn.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Bean","topping","Caned bean",0.3,load_file("C:/Pictures/Toppings/bean.png"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Eggs","topping","Boiled egg slices",0.4,load_file("C:/Pictures/Toppings/egg.jpg"));
/*cheese*/                        
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Mozzarella","cheese","Shreded mozzarela",0.5,load_file("C:/Pictures/Cheese/moz.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Mix","cheese","All kind of cheese what you can find in our cheese menu",0.6,load_file("C:/Pictures/Cheese/mix.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Edami","cheese","Regular edami cheese",0.8,load_file("C:/Pictures/Cheese/edami.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Parmegano","cheese","Finest parmegano what you can find in the world. You should really try this.",1.9,load_file("C:/Pictures/Cheese/parm.jpg"));
insert into pizzamaker.foods(name,type,description,price,rawpicture) values("Trapist","cheese","Regular trapist cheese",0.4,load_file("C:/Pictures/Cheese/trap.jpg"));

select id,name,description,price from pizzamaker.foods;