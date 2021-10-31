# Объектно-ориентированные технологии

Лекции и материалы по курсу ООТ в ЧелГУ, группы МИВТ-101, МИТ-101.

## Лекции

### 1. Введение в ООТ

[Презентация](https://docs.google.com/presentation/d/1UdTBjtRN3Tfz0RLeeLpliwhoMFWcr1dBkioqvQTOpzI/edit?usp=sharing)

**Литература**
1. Стив Макконнелл. Совершенный код
2. Эндрю Троелсен. Язык программирования C#
3. Курсы по C# на https://ulearn.me/

**Задача**

В компании работают менеджеры, техники и водители:

* Менеджеры и техники получают фиксированную ежемесячную зарплату.
* У водителей оплата почасовая.
* Заработная плата техников и водителей зависит от их категории (A, B или C). Категория предоставляет коэффициент от базовой зарплаты: A - 125%, B - 115%, С - 100%.
* Все работники могут получить фиксированную премию за месяц, которая прибавляется к их основной заработной плате.

Напишите программу, которая получает информацию о работниках компании и вычисляет, сколько компания должна выплатить своим сотрудникам в конце месяца.

[Код решения](exercises/01-payroll/Payroll/Program.cs)


### 2. Принципы разработки ПО

[Презентация](https://docs.google.com/presentation/d/1QKmXR3drmNafjhGuEWrdQ_M-KYxlRo_E2Iw5oHY97fE/edit?usp=sharing)

**Литература**
1. Эндрю Хант, Дэвид Томмс. Программист - прагматик. Путь от подмастерья к мастеру
2. [97 вещей, которые должен знать каждый программист](https://97-things-every-x-should-know.gitbooks.io/97-things-every-programmer-should-know/content/ru/)


### 3. Проектирование модулей

[Презентация](https://docs.google.com/presentation/d/1nWGsO0tzJlfhevSeMdg6jNNuAkZljAY48yvbTNAcIRo/edit?usp=sharing)

**Задача**

Написать консольный туду-лист с возможностью сохранять состояние в файле.

Код для туду-листа уже есть, надо разделить его на модули.

[Код, который надо отрефакторить](exercises/03-todo-list)


### 4. Принципы SOLID

[Презентация](https://docs.google.com/presentation/d/1UNpF0XrCIcbwJJicYpqEDDV-u0R0oRP1OGJaPDLUTR0/edit?usp=sharing)

**Литература**
1. Роберт Мартин. Принципы, паттерны и методики гибкой разработки на языке C#
2. [Серия постов Сергея Теплякова про SOLID](http://sergeyteplyakov.blogspot.com/2014/10/solid.html)
3. [Серия постов Александра Бындю про SOLID](https://blog.byndyu.ru/2009/12/blog-post.html)


### 5. Паттерны проектирования

[Презентация](https://docs.google.com/presentation/d/1MN_o1ZMMi3R6vvrvvIGFTNVlAkltMveU_vwmGYRCZ0Y/edit?usp=sharing)

**Литература**
1. Эрих Гамма, Ричард Хелм, Ральф Джонсон, Джон Влиссидес. Приемы объектно-ориентированного проектирования. Паттерны проектирования
2. Эрик Фримен, Элизабет Фримен, Кэтти Сьерра, Берт Бейтс. Паттерны проектирования
3. [Паттерны проектирования на refactoring.guru](https://refactoring.guru/ru/design-patterns)


### 6. Практика по паттернам

[Репозиторий с задачами](https://github.com/csu-iit/programming-Patterms.CSharp)


## Практика

### Задача 1. Родственные связи

Напишите программу, которая моделирует родственные связи. Программа позволяет создать объекты типа `Person` и указывать, кто из людей кому является родителем и кто с кем состоит в браке.

Должны быть функции, позволяющие для каждого человека получить список:
* Родителей
* Двоюродных братьев и сестер
* Дядюшек и тетушек
* In-laws (cвекра и свекрови или тестя и тещи)


### Задача 2. Книжный магазин

Реализовать модуль корзины для вычисления общей стоимости заказа в книжном интернет-магазине:

1. Два вида книг: бумажные и электронные
2. Для бумажных книг доставка от 1000 рублей бесплатная, иначе 200 рублей. Для электронных всегда бесплатная
3. Есть промокоды: на конкретную книгу бесплатно, на бесплатную доставку, на скидку X%, на скидку X рублей
4. Есть акция: при покупке двух бумажных книг одного автора одна электронная книга того же автора в подарок
5. Система должна быть расширяемой: легко добавить новые правила, скидки, виды промокодов и книг
