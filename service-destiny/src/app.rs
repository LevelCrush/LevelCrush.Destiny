pub mod activity;
pub mod clan;
pub mod instance;
pub mod member;
pub mod report;
pub mod state;

pub const DESTINY_MODE_GROUPS: &[(&str, &str)] = &[
    ("Raid", "4"),
    ("Dungeons", "82"),
    ("Patrols", "6"),
    ("Endgame PvE", "4,46,47,82"),
    (
        "PvP",
        "10,12,15,19,25,31,37,38,39,41,42,43,44,45,48,49,50,59,60,61,62,65,68,69,70,71,72,73,74,80,81,84,89,90,91",
    ),
    ("PvP Trials", "39,41,42,84"),
    ("PvP Comp", "69,,72, 74"),
];
