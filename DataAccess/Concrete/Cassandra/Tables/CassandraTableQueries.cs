namespace DataAccess.Concrete.Cassandra.Tables
{
    public static class CassandraTableQueries
    {
        public static string AdvEvents =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.adv_events(id bigint, client_id bigint, project_id bigint, customer_id bigint, level_name text, level_index int, adv_type text, in_minutes decimal, trigger_time date, status boolean, PRIMARY KEY(id))";

        public static string BuyingEvents =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.buying_events(id bigint, client_id bigint, project_id bigint, customer_id bigint, level_name text, level_index int, product_type text, in_minutes decimal, triggered_time date, status boolean, PRIMARY KEY(id))";

        public static string EnemyBaseLoginLevelModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.enemy_base_login_level_models(id bigint, client_id bigint, project_id bigint, customer_id bigint, level_name text, level_index int, playing_time int, average_scores int, date_time date, is_dead tinyint, total_power_usage int,  status boolean, PRIMARY KEY(id))";

        public static string EnemyBaseLevelFailModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.enemy_base_level_fail_models(id bigint, client_id bigint, project_id bigint, customer_id bigint, fail_time_after_level_starting int, level_name text, level_index int, diying_difficulty_level int, fail_location_x decimal, fail_location_y decimal, fail_location_z decimal, date_time date,  status boolean, PRIMARY KEY(id))";

        public static string GameSessionModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.game_session_models(id bigint, client_id bigint, project_id bigint, customer_id bigint, session_start_time date, session_finish_time date, session_time_minute decimal, status boolean, PRIMARY KEY(id))";

        public static string LevelBaseSessionModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.level_base_session_models(id bigint, client_id bigint, project_id bigint, customer_id bigint, level_name text, level_index int, difficulty_level int, session_time_minute decimal, session_start_time date, session_finish_time date, status boolean, PRIMARY KEY(id))";

        public static string ClientDataModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.client_data_models(id bigint, project_id bigint, is_paid_client tinyint, created_at date, paid_time date,  status boolean, PRIMARY KEY(id))";

        public static string ChurnBlokerMlResults =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.churn_bloker_ml_results(id bigint, client_id bigint, project_id bigint, customer_id bigint, model_type text, model_result double, date_time date, status boolean, PRIMARY KEY(id))";

        public static string ChurnDates =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.churn_dates(id bigint, project_id bigint, churn_date_minutes bigint, date_type_on_gui text, status boolean, PRIMARY KEY(id))";

        public static string OfferBehaviorModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.offer_behavior_models(id bigint, client_id bigint, project_id bigint, customer_id bigint, version smallint, offer_id int, date_time date, isBuy_offer tinyint, status boolean, PRIMARY KEY(id))";

        public static string ChurnClientPredictionResults =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.churn_prediction_ml_result_models(id bigint, client_id bigint, project_id bigint, customer_id bigint, model_type text, model_result decimal, date_time date, status boolean, PRIMARY KEY(id))";

        public static string AdvStrategyBehaviorModels =>
            "CREATE TABLE IF NOT EXISTS ClientDatabase.adv_strategy_behavior_models(id bigint, client_id bigint, project_id bigint, version int, name text, date_time date, status boolean, PRIMARY KEY(id))";
    }
}