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

INSERT INTO cities (id, name) VALUES (1, 'London');
INSERT INTO cities (id, name) VALUES (2, 'Gotham');
INSERT INTO cities (id, name) VALUES (3, 'Metropolis');
INSERT INTO cities (id, name) VALUES (4, 'Coast City');

INSERT INTO heroes (id, name, city_id) VALUES (2, 'Wonder Woman', 1);
INSERT INTO heroes (id, name, city_id) VALUES (3, 'Superman', 3);
INSERT INTO heroes (id, name, city_id) VALUES (1, 'Batman', 2);

INSERT INTO powers (id, name, description) VALUES (1, 'Super strength', 'Super strong muscles');
INSERT INTO powers (id, name, description) VALUES (2, 'Heat vision', 'Shoot laser beams from the eyes');
INSERT INTO powers (id, name, description) VALUES (3, 'Flight', 'Can fly through the air');
INSERT INTO powers (id, name, description) VALUES (4, 'Batman', 'Enough said');

INSERT INTO heroes_powers (id, hero_id, power_id) VALUES (1, 1, 4);
INSERT INTO heroes_powers (id, hero_id, power_id) VALUES (2, 2, 1);
INSERT INTO heroes_powers (id, hero_id, power_id) VALUES (3, 2, 3);
INSERT INTO heroes_powers (id, hero_id, power_id) VALUES (4, 3, 1);
INSERT INTO heroes_powers (id, hero_id, power_id) VALUES (5, 3, 2);
INSERT INTO heroes_powers (id, hero_id, power_id) VALUES (6, 3, 3);