drop schema if exists pizzamaker;
create schema pizzamaker;
create table pizzamaker.sauce
(
id int  primary key AUTO_INCREMENT,
name varchar(100)  not null unique,
description varchar(1000)  not null,
price  double not null default 0,
rawpicture mediumblob  not null
);