namespace DataAccess.Concrete.Cassandra.Tables
{
    public static class CassandraTableQueries
    {
        public static string AdvEvents => "CREATE TABLE IF NOT EXISTS adv_events(id bigint, client_id bigint, project_id bigint, customer_id bigint, triggers_level_name text, adv_type text, difficulty_level int, in_minutes decimal, trigger_time date, status boolean, PRIMARY KEY(id))";
        public static string BuyingEvents => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, triggers_level_name text, product_type text, difficulty_level int, in_minutes decimal, trigger_time date, status boolean, PRIMARY KEY(id))";
        public static string EveryLoginLevelDatas => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, level_name text, levels_difficulty_level int, playing_time int, average_scores int, date_time date, is_dead tinyint, total_power_usage int,  status boolean, PRIMARY KEY(id))";
        public static string LevelBaseDieDatas => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, diying_time_after_level_starting int, level_name text, diying_difficulty_level int, diying_location_x decimal, diying_location_y decimal, diying_location_z decimal, date_time date,  status boolean, PRIMARY KEY(id))";
        public static string GameSessionEveryLoginDatas => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, session_start_time date, session_finish_time date, session_time_minute decimal, status boolean, PRIMARY KEY(id))";
        public static string LevelBaseSessionDatas => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, level_name text, difficulty_level int, session_time_minute decimal, session_start_time date, session_finish_time date, status boolean, PRIMARY KEY(id))";
        public static string ClientDataModels => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, project_id bigint, is_paid_client tinyint, created_at date, paid_time date,  status boolean, PRIMARY KEY(id))";
        public static string ChurnBlokerMlResults => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, result_value double, date_time date, status boolean, PRIMARY KEY(id))";
        public static string ChurnDates => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, project_id bigint, churn_date_minutes bigint, date_type_on_gui text, status boolean, PRIMARY KEY(id))";
        public static string OfferBehaviorModels => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, customer_id bigint, version smallint, offer_name text, date_time date, isBuy_offer tinyint, status boolean, PRIMARY KEY(id))";
        public static string ChurnClientPredictionResults => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, ChurnPredictionDate date, status boolean, PRIMARY KEY(id))";
        public static string AdvStrategyBehaviorModels => "CREATE TABLE IF NOT EXISTS user_projects(id bigint, client_id bigint, project_id bigint, version int, name text, date_time date, status boolean, PRIMARY KEY(id))";
    }
}


