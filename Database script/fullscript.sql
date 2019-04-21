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

insert into pizzamaker.dough(name,description,price,rawpicture) values("Normal dough","This is a regular dough where we add all the necessary ingridients what is needed to a good pizza dough",0.99,load_file("C:/Pictures/Doughs/normal.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Wholegrain dough","This is made by wholgrain ingredients which is good for your health.",1.35,load_file("C:/Pictures/Doughs/wholegrain.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Multigrain dough","This is made by multigrain ingredients which is good for your health. The nuts are good aminoacid source.",1.50,load_file("C:/Pictures/Doughs/multigrain.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Glutenfree dough","This is made by glutemfree ingredients we strongly recommand for those people who has gluten intolarance",2.0,load_file("C:/Pictures/Doughs/glutenfree.jpg"));
insert into pizzamaker.dough(name,description,price,rawpicture) values("Crusty dough","This is a regular dough where we add all the necessary ingridients what is needed to a good pizza dough with crusty end.",1.25,load_file("C:/Pictures/Doughs/normal.jpg"));