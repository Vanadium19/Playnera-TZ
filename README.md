Техническое задание для Playnera

Библиотеки проекта

1) Dotween - для создание анимации подбора предмета и мягкого приземления на полку.
2) Zenject - для управления зависимостями
3) R3 - для отслеживания позиции курсора и для связи между моделью и представлением.

Архитектура проекта

Ядро проекта разделено на 4 слоя: Контроллеры -> модель -> презенторы -> вью.
Для связи представления и модели был использован MVP-Passive-View.
Модель ничего не знает о представлении, представление ничего не знает о модели.

Тезническое задание

1) Реализация механики Drag-and-Drop ✓
2) Прокрутка изображения сцены (опционально) ✓
