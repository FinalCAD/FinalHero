create table powers
(
    id          serial not null
        constraint powers_pk
            primary key,
    name        varchar,
    description varchar
);

create unique index powers_id_uindex
    on powers (id);

create table cities
(
    id   serial not null
        constraint cities_pk
            primary key,
    name varchar
);

create table heroes
(
    id      serial not null
        constraint users_pk
            primary key,
    name    varchar,
    city_id integer
        constraint heroes_cities_id_fk
            references cities
);

create unique index users_id_uindex
    on heroes (id);

create table heroes_powers
(
    id       serial not null
        constraint heroes_powers_pk
            primary key,
    hero_id  integer
        constraint heroes_powers_heroes_id_fk
            references heroes,
    power_id integer
        constraint heroes_powers_powers_id_fk
            references powers
);

create unique index heroes_powers_id_uindex
    on heroes_powers (id);

create unique index cities_id_uindex
    on cities (id);

